using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using AddonHelper;
using System.Diagnostics;
using MrAG.OAuth;

namespace Imgur {
    public class Imgur : Addon {
        public Settings settings;

        public NotifyIcon Tray;
        public string imgurApiKey = "1caece560704b3ee19d7b112e63ea2ac";
        public string imgurConsumerKey = "0d3b44ffdecba1c44057fbb8b78f2c4204fcb7c1f";
        public string imgurConsumerSecret = "62842910a06d90f793d773657bfae555";

        public bool authenticated;
        public bool isPro;
        public string username;

        public string imageFormat = "PNG";

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Bitmap bmpIcon;

        public OAuth oauth;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/Imgur/settings.txt");
            this.bmpIcon = new Icon("Addons/Imgur/Icon.ico").ToBitmap();

            LoadSettings();
        }

        public void LoadSettings() {
            if (settings.Contains("ShortcutModifiers")) {
                // Migrate old 3.00 config file
                settings.SetString("ShortcutDragModifiers", settings.GetString("ShortcutModifiers"));
                settings.SetString("ShortcutDragKey", settings.GetString("ShortcutKey"));
                settings.SetString("ShortcutPasteModifiers", "");
                settings.SetString("ShortcutPasteKey", "");

                settings.Delete("ShortcutModifiers");
                settings.Delete("ShortcutKey");

                settings.Save();
            }

            if (!settings.Contains("AccessToken")) {
                // Migrate old 3.10 config file
                settings.SetString("Username", "");
                settings.SetBool("IsPro", false);
                settings.SetString("AccessToken", "");
                settings.SetString("AccessTokenSecret", "");

                settings.Save();
            }

            imageFormat = settings.GetString("Format");

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");

            this.oauth = new OAuth("https://" + "api.imgur.com/oauth/", this.imgurConsumerKey, this.imgurConsumerSecret);
            this.oauth.ServiceIcon = new Icon("Addons/Imgur/Icon.ico");
            this.oauth.ServiceName = "Imgur";

            this.username = settings.GetString("Username");
            this.isPro = settings.GetBool("IsPro");
            this.oauth.AccessToken = settings.GetString("AccessToken");
            this.oauth.AccessTokenSecret = settings.GetString("AccessTokenSecret");
            authenticated = username != "" && this.oauth.AccessToken != "";
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable DragItem = new Hashtable();
            DragItem.Add("Visible", true);
            DragItem.Add("Text", "Drag -> Imgur" + (this.authenticated ? " (" + this.username + ")" : ""));
            DragItem.Add("Image", this.bmpIcon);
            DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
            DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
            DragItem.Add("ShortcutKey", this.shortCutDragKey);
            ret.Add(DragItem);

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList());
            UpItem.Add("Text", "Imgur" + (this.authenticated ? " (" + this.username + ")" : ""));
            UpItem.Add("Image", this.bmpIcon);
            UpItem.Add("Action", new Action(Upload));
            UpItem.Add("ShortcutModifiers", this.shortCutPasteModifiers);
            UpItem.Add("ShortcutKey", this.shortCutPasteKey);
            ret.Add(UpItem);

            return ret.ToArray();
        }

        public void Settings() {
            new FormSettings(this).ShowDialog();
        }

        public void DragCallback(DragCallback callback) {
            switch (callback.Type) {
                case DragCallbackType.Image:
                    UploadImage(callback.Image);
                    break;

                case DragCallbackType.Animation:
                    UploadAnimation(callback.Animation);
                    break;
            }
        }

        public void UploadImage(Image img) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Imgur/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            switch (imageFormat.ToLower()) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            img.Save(ms, format);
            img.Dispose();

            string url = this.UploadToImgur(ms);

            if (url != "CANCELED" && url != "")
                this.Backup(ms.GetBuffer(), url.Split('/', '\\').Last());
            else
                this.Backup(ms.GetBuffer(), this.RandomFilename(5) + "." + imageFormat.ToLower());

            if (url != "CANCELED") {
                if (url != "") {
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to Imgur and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on Imgur's side. Try again.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadAnimation(MemoryStream ms) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Imgur/Icon.ico");

            string url = this.UploadToImgur(ms);
            if (url != "CANCELED" && url != "")
                this.Backup(ms.GetBuffer(), url.Split('/', '\\').Last());
            else
                this.Backup(ms.GetBuffer(), this.RandomFilename(5) + ".gif");

            if (url != "CANCELED") {
                if (url != "") {
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Animation uploaded to Imgur and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on Imgur's side. Try again.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadImages(StringCollection files) {
            if (files.Count == 0)
                return;

            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Imgur/Icon.ico");

            string finalCopy = "";
            foreach (string file in files) {
                if (!(file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".gif")))
                    continue;

                MemoryStream ms = new MemoryStream(File.ReadAllBytes(file));

                string url = this.UploadToImgur(ms);
                if (url == "CANCELED") {
                    finalCopy = "CANCELED";
                    break;
                }
                if (url != "") {
                    finalCopy += url + "\n";
                }
            }

            if (finalCopy != "CANCELED") {
                if (finalCopy != "") {
                    this.SetClipboardText(finalCopy.Substring(0, finalCopy.Length - 1));
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image(s) uploaded to Imgur and URL(s) copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "You didn't copy any images, or the image format is not supported.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public string UploadToImgur(MemoryStream ms) {
            byte[] writeData;
            if (this.authenticated)
                writeData = Encoding.ASCII.GetBytes("image=" + LongDataEscape(Convert.ToBase64String(ms.ToArray())));
            else
                writeData = Encoding.ASCII.GetBytes("key=" + imgurApiKey + "&image=" + LongDataEscape(Convert.ToBase64String(ms.ToArray())));

            this.ProgressBar.Start("Imgur", writeData.Length);

            HttpWebRequest req = null;
            string url = "";
            try {
                if (this.authenticated)
                    req = this.oauth.AuthenticatedWebRequest("https://" + "api.imgur.com/2/upload.xml");
                else
                    req = (HttpWebRequest)HttpWebRequest.Create("https://" + "api.imgur.com/2/upload.xml");
                req.Proxy = this.GetProxy();
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = writeData.Length;
                req.UserAgent = "ClipUpload 3/clipupload.net";

                Stream stream = req.GetRequestStream();

                ms.Dispose();
                ms = new MemoryStream(writeData);
                int sr = 1024;
                for (int i = 0; i < ms.Length; i += 1024) {
                    if (ms.Length - i < 1024)
                        sr = (int)ms.Length - i;
                    else
                        sr = 1024;

                    byte[] buffer = new byte[sr];
                    ms.Seek((long)i, SeekOrigin.Begin);
                    ms.Read(buffer, 0, sr);
                    stream.Write(buffer, 0, sr);

                    if (this.ProgressBar.Canceled) {
                        req.Abort();
                        ms.Dispose();

                        req = null;
                        ms = null;

                        return "CANCELED";
                    }
                    this.ProgressBar.Set(i);
                }

                WebResponse response = req.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();

                if (!result.Contains("stat=\"fail\"")) {
                    url = GetBetween(result, "<original>", "</original>");
                    this.AddLog(url);
                }
            } catch (Exception ex) {
                this.Debug("UploadToImgur threw " + ex.GetType().FullName + ": '" + ex.Message + "'");
            }

            this.ProgressBar.Done();

            return url;
        }

        public void Upload() {
            if (Clipboard.ContainsImage())
                UploadImage(Clipboard.GetImage());
            else if (Clipboard.ContainsFileDropList())
                UploadImages(Clipboard.GetFileDropList());
        }
    }
}

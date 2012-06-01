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

namespace PostHttp {
    public class PostHttp : Addon {
        public Settings settings;

        public NotifyIcon Tray;

        public string imageFormat = "PNG";
        public string endpointURL = "";
        public string endpointName = "";

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Bitmap bmpIcon;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/PostHttp/settings.txt");
            this.bmpIcon = new Icon("Addons/PostHttp/Icon.ico").ToBitmap();

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

            imageFormat = settings.GetString("Format");
            endpointURL = settings.GetString("EndpointURL");
            endpointName = settings.GetString("EndpointName");

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable DragItem = new Hashtable();
            DragItem.Add("Visible", true);
            DragItem.Add("Text", "Drag -> " + this.endpointName);
            DragItem.Add("Image", this.bmpIcon);
            DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
            DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
            DragItem.Add("ShortcutKey", this.shortCutDragKey);
            ret.Add(DragItem);

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList() || Clipboard.ContainsText());
            UpItem.Add("Text", this.endpointName);
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
            Tray.Icon = new Icon("Addons/PostHttp/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            switch (imageFormat.ToLower()) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            img.Save(ms, format);

            string result = UploadToEndPoint(ms, this.endpointName);

            if (result != "CANCELED") {
                string filename = "";

                if (result.StartsWith("http")) {
                    this.SetClipboardText(result);
                    this.AddLog(result);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to " + endpointName + " and URL copied to clipboard.", ToolTipIcon.Info);

                    filename = result.Split('/', '\\').Last();
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on " + endpointName + "'s side. Try again.", ToolTipIcon.Error);
                }

                if (filename != "")
                    this.Backup(ms.GetBuffer(), filename);
                else
                    this.Backup(ms.GetBuffer(), this.RandomFilename(5) + "." + imageFormat.ToLower());
            }

            Tray.Icon = defIcon;
        }

        public void UploadAnimation(MemoryStream ms) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/PostHttp/Icon.ico");

            string result = UploadToEndPoint(ms, this.endpointName);

            if (result != "CANCELED") {
                string filename = "";

                if (result.StartsWith("http")) {
                    this.SetClipboardText(result);
                    this.AddLog(result);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Animation uploaded to " + endpointName + " and URL copied to clipboard.", ToolTipIcon.Info);

                    filename = result.Split('/', '\\').Last();
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on " + endpointName + "'s side. Try again.", ToolTipIcon.Error);
                }

                if (filename != "")
                    this.Backup(ms.GetBuffer(), filename);
                else
                    this.Backup(ms.GetBuffer(), this.RandomFilename(5) + ".gif");
            }

            Tray.Icon = defIcon;
        }

        public void UploadText(string Text) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/PostHttp/Icon.ico");

            string result = UploadToEndPoint(new MemoryStream(Encoding.ASCII.GetBytes(Text)), this.endpointName);

            if (result != "CANCELED") {
                string filename = "";

                if (result.StartsWith("http")) {
                    this.SetClipboardText(result);
                    this.AddLog(result);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to " + endpointName + " and URL copied to clipboard.", ToolTipIcon.Info);

                    filename = result.Split('/', '\\').Last();
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on " + endpointName + "'s side. Try again.", ToolTipIcon.Error);
                }

                if (filename != "")
                    this.Backup(Encoding.ASCII.GetBytes(Text), filename);
                else
                    this.Backup(Encoding.ASCII.GetBytes(Text), this.RandomFilename(5) + "." + imageFormat.ToLower());
            }

            Tray.Icon = defIcon;
        }

        public void UploadFiles(StringCollection files) {
            if (files.Count == 0)
                return;

            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/PostHttp/Icon.ico");

            string finalCopy = "";
            foreach (string file in files) {
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(file));

                string result = UploadToEndPoint(ms, file);
                if (result == "CANCELED") {
                    finalCopy = "CANCELED";
                    break;
                }
                if (result.StartsWith("http")) {
                    this.AddLog(result);
                    finalCopy += result + "\n";
                }
            }

            if (finalCopy != "CANCELED") {
                if (finalCopy != "") {
                    this.SetClipboardText(finalCopy.Substring(0, finalCopy.Length - 1));
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image(s) uploaded to " + endpointName + " and URL(s) copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "You didn't copy any files or " + endpointName + "'s server didn't give an appropriate reply.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public string UploadToEndPoint(MemoryStream ms, string filename) {
            string Filename = Uri.EscapeDataString(filename.Split('/', '\\').Last());
            byte[] writeData = Encoding.ASCII.GetBytes((filename == this.endpointName ? "" : "filename=" + Filename) + "&file=" + LongDataEscape(Convert.ToBase64String(ms.ToArray())));

            this.ProgressBar.Start(filename, writeData.Length);

            string result = "";
            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.endpointURL);
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
                result = reader.ReadToEnd();
            } catch { }

            this.ProgressBar.Done();

            return result;
        }

        public void Upload() {
            if (Clipboard.ContainsImage())
                UploadImage(Clipboard.GetImage());
            else if (Clipboard.ContainsText())
                UploadText(Clipboard.GetText());
            else if (Clipboard.ContainsFileDropList())
                UploadFiles(Clipboard.GetFileDropList());
        }
    }
}

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

namespace Facebook {
    public class Facebook : Addon {
        public Settings settings;

        public NotifyIcon Tray;

        public string facebookAppID = "294548907266945";
        public string facebookName = "";
        public FacebookClient facebookClient;

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Image bmpIcon;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/Facebook/settings.txt");
            this.bmpIcon = Image.FromFile("Addons/Facebook/Icon.ico");

            this.facebookClient = new FacebookClient() { IsSecureConnection = true };

            LoadSettings();

            if (this.facebookClient.AccessToken != "") {
                this.facebookName = (this.facebookClient.Get("/me") as dynamic).name;
            }
        }

        public void LoadSettings() {
            facebookClient.AccessToken = settings.GetString("AccessToken");

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable DragItem = new Hashtable();
            DragItem.Add("Visible", this.facebookClient.AccessToken != "");
            DragItem.Add("Text", "Drag -> Facebook");
            DragItem.Add("Image", this.bmpIcon);
            DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
            DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
            DragItem.Add("ShortcutKey", this.shortCutDragKey);
            ret.Add(DragItem);

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", this.facebookClient.AccessToken != "" && (Clipboard.ContainsImage() || Clipboard.ContainsFileDropList()));
            UpItem.Add("Text", "Facebook");
            UpItem.Add("Image", this.bmpIcon);
            UpItem.Add("Action", new Action(Upload));
            UpItem.Add("ShortcutModifiers", this.shortCutPasteModifiers);
            UpItem.Add("ShortcutKey", this.shortCutPasteKey);
            ret.Add(UpItem);

            Hashtable AuthItem = new Hashtable();
            AuthItem.Add("Visible", this.facebookClient.AccessToken == "");
            AuthItem.Add("Text", "Authenticate Facebook");
            AuthItem.Add("Image", this.bmpIcon);
            AuthItem.Add("Action", new Action(Settings));
            ret.Add(AuthItem);

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
            }
        }

        public void UploadImage(Image img) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Facebook/Icon.ico");

            MemoryStream ms = new MemoryStream();

            img.Save(ms, ImageFormat.Png);
            img.Dispose();

            string url = this.UploadToFacebook(ms);

            if (url != "")
                this.Backup(ms.GetBuffer(), url.Split('/', '\\').Last() + ".png");
            else
                this.Backup(ms.GetBuffer(), this.RandomFilename(5) + ".png");

            if (url != "CANCELED") {
                if (url != "") {
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to Facebook and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on Facebook's side. Try again.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadImages(StringCollection files) {
            if (files.Count == 0)
                return;

            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Facebook/Icon.ico");

            string finalCopy = "";
            foreach (string file in files) {
                if (!(file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".gif")))
                    continue;

                MemoryStream ms = new MemoryStream(File.ReadAllBytes(file));

                string url = this.UploadToFacebook(ms);
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
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image(s) uploaded to Facebook and URL(s) copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "You didn't copy any images, or the image format is not supported.", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public string UploadToFacebook(MemoryStream ms) {
            // TODO: Make progressbar functional for C# Facebook SDK.
            //       Limitation in current C# Facebook SDK makes us unable to track progress of photo uploads.
            this.ProgressBar.Start("Facebook", ms.Length);

            string url = "";
            try {
                ms.Seek(0, SeekOrigin.Begin);

                FacebookMediaStream img = new FacebookMediaStream();
                img.ContentType = "image/png";
                img.FileName = this.RandomFilename(5) + ".png";
                img.SetValue(ms);

                Dictionary<string, object> photoParams = new Dictionary<string, object>();
                // TODO: Some way to attach a message to an image
                //photoParams["message"] = message;
                photoParams["image"] = img;
                dynamic ret = this.facebookClient.Post("/me/photos", photoParams);

                string photoID = ret.id;
                url = "https://" + "www.facebook.com/photo.php?fbid=" + photoID;

                this.AddLog(url);
            } catch { }

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

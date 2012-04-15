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

namespace Snaggy {
    public class Snaggy : Addon {
        public Settings settings;

        public NotifyIcon Tray;

        public string imageFormat = "PNG";

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Bitmap bmpIcon;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/Snaggy/settings.txt");
            this.bmpIcon = new Icon("Addons/Snaggy/Icon.ico").ToBitmap();

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

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable DragItem = new Hashtable();
            DragItem.Add("Visible", true);
            DragItem.Add("Text", "Drag -> Snag.gy");
            DragItem.Add("Image", this.bmpIcon);
            DragItem.Add("Action", new Action(delegate { this.Drag(new Action<Image>(DragUpload)); }));
            DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
            DragItem.Add("ShortcutKey", this.shortCutDragKey);
            ret.Add(DragItem);

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList());
            UpItem.Add("Text", "Snag.gy");
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

        public string UploadImage(Image img) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Snaggy/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            switch (imageFormat.ToLower()) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            img.Save(ms, format);

            WebClient wc = new WebClient();
            wc.Proxy = this.GetProxy();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "ClipUpload 3");
            string result = "";
            try {
                result = wc.UploadString("http://" + "snag.gy/assets/server-scripts/makeImage.php?mode=1", Convert.ToBase64String(ms.ToArray()));
            } catch { return ""; }

            if (result.Substring(0, 2) == "#E") {
                int errNum = int.Parse(result[2].ToString());
                string errMsg = "Something broke and it's not your fault. Try pasting again.";
                switch (errNum) {
                    case 3: errMsg = "Snaggy is unable to read the file that you are trying to upload."; break;
                    case 4: errMsg = "Snaggy was unable to read the data from your clipboard."; break;
                    case 5: errMsg = "It looks like you tried to copy part of a webpage, but no image could be found."; break;
                    case 6: errMsg = "It doesn't look like there's any image data on your clipboard."; break;
                    case 7: errMsg = "The image is too large for Snaggy to handle!"; break;
                    case 8: errMsg = "Snaggy seems to be having problems connecting with the image uploading server."; break;
                    case 9: errMsg = "Snaggy seems to be having problems uploading the image to our servers."; break;
                }
                Tray.ShowBalloonTip(5000, "Snaggy upload failed", errMsg, ToolTipIcon.Error);

                return "";
            }

            Tray.Icon = defIcon;

            string url = "http://" + "i.snag.gy/" + result + ".jpg";
            this.AddLog(url);
            return url;
        }

        public void UploadImages(StringCollection files) {
            if (files.Count == 0)
                return;

            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Snaggy/Icon.ico");

            string finalCopy = "";
            foreach (string file in files) {
                if (!(file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".gif")))
                    continue;

                MemoryStream ms = new MemoryStream();

                Image img = Image.FromFile(file);
                string result = UploadImage(img);

                if (result != "")
                    finalCopy += result + "\n";
            }

            if (finalCopy != "") {
                this.SetClipboardText(finalCopy.Substring(0, finalCopy.Length - 1));
                Tray.ShowBalloonTip(1000, "Upload success!", "Image(s) uploaded to Snaggy and URL(s) copied to clipboard.", ToolTipIcon.Info);
            } else
                Tray.ShowBalloonTip(1000, "Upload failed!", "You didn't copy any images, or the image format is not supported.", ToolTipIcon.Error);

            Tray.Icon = defIcon;
        }

        public void Upload() {
            if (Clipboard.ContainsImage()) {
                string result = UploadImage(Clipboard.GetImage());
                if (result != "") {
                    this.SetClipboardText(result);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to Snaggy and URL copied to clipboard.", ToolTipIcon.Info);
                }
            } else if (Clipboard.ContainsFileDropList())
                UploadImages(Clipboard.GetFileDropList());
        }

        public void DragUpload(Image img) {
            string result = UploadImage(img);
            if (result != "") {
                this.SetClipboardText(result);
                Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to Snaggy and URL copied to clipboard.", ToolTipIcon.Info);
            }
        }
    }
}

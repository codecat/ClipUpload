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

namespace ClipBoard {
    public class ClipBoard : Addon
    {
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
            this.settings = new Settings("Addons/ClipBoard/settings.txt");
            this.bmpIcon = new Icon("Addons/ClipBoard/Icon.ico").ToBitmap();

            LoadSettings();
        }

        public void LoadSettings() {
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
            DragItem.Add("Text", "Drag -> ClipBoard");
            DragItem.Add("Image", this.bmpIcon);
            DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
            DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
            DragItem.Add("ShortcutKey", this.shortCutDragKey);
            ret.Add(DragItem);

            return ret.ToArray();
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
            Tray.Icon = new Icon("Addons/ClipBoard/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            switch (imageFormat.ToLower()) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            img.Save(ms, format);
            Clipboard.SetImage(img);
            img.Dispose();
            
            Tray.ShowBalloonTip(1000, "Copy success!", "Image copied to clipboard.", ToolTipIcon.Info);

            Tray.Icon = defIcon;
        }


        public void UploadImages(StringCollection files) {
                return;
        }

        public void Upload() {
            if (Clipboard.ContainsImage())
                UploadImage(Clipboard.GetImage());
            else if (Clipboard.ContainsFileDropList())
                UploadImages(Clipboard.GetFileDropList());
        }
    }
}

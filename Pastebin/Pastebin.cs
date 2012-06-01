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

namespace Pastebin {
    public class Pastebin : Addon {
        public Settings settings;

        public NotifyIcon Tray;
        public string PastebinAPIKey = "d88817b8669cb8b1e90c5a4e1ed4f64a";

        public bool UserLoggedIn = false;
        public string UserKey = "";
        public string UserName = "";
        public bool ShowPrivate;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;

            this.settings = new Settings("Addons/Pastebin/settings.txt");

            LoadSettings();
        }

        public void LoadSettings() {
            UserKey = settings.GetString("UserKey");
            UserName = settings.GetString("UserName");
            ShowPrivate = settings.GetBool("ShowPrivate");
            UserLoggedIn = UserKey.Length == 32;
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", Clipboard.ContainsText() || Clipboard.ContainsFileDropList());
            UpItem.Add("Text", "Clipboard -> Pastebin");
            UpItem.Add("Image", new Icon("Addons/Pastebin/Icon.ico").ToBitmap());
            UpItem.Add("Action", new Action(Upload));
            ret.Add(UpItem);

            Hashtable UpItemPrivate = new Hashtable();
            UpItemPrivate.Add("Visible", ShowPrivate && (Clipboard.ContainsText() || Clipboard.ContainsFileDropList()));
            UpItemPrivate.Add("Text", "Clipboard -> Pastebin (Private)");
            UpItemPrivate.Add("Image", new Icon("Addons/Pastebin/Icon.ico").ToBitmap());
            UpItemPrivate.Add("Action", new Action(UploadPrivate));
            ret.Add(UpItemPrivate);

            Hashtable UpItemRaw = new Hashtable();
            UpItemRaw.Add("Visible", Clipboard.ContainsText() || Clipboard.ContainsFileDropList());
            UpItemRaw.Add("Text", "Clipboard -> Raw Pastebin");
            UpItemRaw.Add("Image", new Icon("Addons/Pastebin/Icon.ico").ToBitmap());
            UpItemRaw.Add("Action", new Action(RawUpload));
            ret.Add(UpItemRaw);

            Hashtable UpItemRawPrivate = new Hashtable();
            UpItemRawPrivate.Add("Visible", ShowPrivate && (Clipboard.ContainsText() || Clipboard.ContainsFileDropList()));
            UpItemRawPrivate.Add("Text", "Clipboard -> Raw Pastebin (Private)");
            UpItemRawPrivate.Add("Image", new Icon("Addons/Pastebin/Icon.ico").ToBitmap());
            UpItemRawPrivate.Add("Action", new Action(RawUploadPrivate));
            ret.Add(UpItemRawPrivate);

            return ret.ToArray();
        }

        public void Settings() {
            new FormSettings(this).ShowDialog();
        }

        public string UploadText(string Content, bool Private) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/Pastebin/Icon.ico");

            WebClient wc = new WebClient();
            wc.Proxy = this.GetProxy();
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string result = "Bad API request, no result";
            try {
                string args = "";

                args += "api_dev_key=" + PastebinAPIKey;
                args += "&api_option=paste";
                if (UserLoggedIn)
                    args += "&api_user_key=" + UserKey;
                args += "&api_paste_code=" + LongDataEscape(Content);
                args += "&api_paste_private=" + (Private ? "1" : "0");

                result = wc.UploadString("http://" + "pastebin.com/api/api_post.php", args);
            } catch { }

            Tray.Icon = defIcon;

            if (!result.Contains("Bad API request, ")) {
                this.AddLog(result);
                Tray.ShowBalloonTip(1000, "Upload success!", "Text uploaded to Pastebin and URL copied to clipboard.", ToolTipIcon.Info);

                return result;
            } else {
                Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on Pastebin's side. Try again.", ToolTipIcon.Error);
                return "";
            }
        }

        private void Up(bool Raw, bool Private) {
            string result = "";

            if (Clipboard.ContainsText())
                result = UploadText(Clipboard.GetText(), Private);
            else if (Clipboard.ContainsFileDropList()) {
                StringCollection files = Clipboard.GetFileDropList();
                if (files.Count == 1)
                    result = UploadText(File.ReadAllText(files[0]), Private);
            }

            if (result != "") {
                if (Raw)
                    this.SetClipboardText(result.Replace("pastebin.com/", "pastebin.com/raw.php?i="));
                else
                    this.SetClipboardText(result);
            }
        }

        public void Upload() {
            Up(false, false);
        }

        public void RawUpload() {
            Up(true, false);
        }

        public void UploadPrivate() {
            Up(false, true);
        }

        public void RawUploadPrivate() {
            Up(true, true);
        }
    }
}

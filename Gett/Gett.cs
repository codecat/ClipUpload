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

/* This is a work in progress.
 * It is not finished, and doesn't work yet.
 * Therefore, it should not be included in builds!
 */

namespace Gett {
    public class Gett : Addon {
        public NotifyIcon Tray;
        public Settings settings;
        public Bitmap bmpIcon;

        public bool UserLoggedIn = false;
        public string UserKey = "";
        public string UserRefresh = "";
        public DateTime UserKeyExpires;
        public string UserName = "";

        public string APIKey = "t1zdsz5h4ojtkpgb94qqf6rbd27jbgldi";
        public string BaseURL = "https://" + "open.ge.tt/1/";

        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/Gett/settings.txt");
            this.bmpIcon = new Icon("Addons/Gett/Icon.ico").ToBitmap();

            LoadSettings();
        }

        public string RequestURL(string action) {
            return BaseURL + action;
        }

        public void LoadSettings() {

        }

        public bool HandleLoginResponse(object result) {
            if (result != null) {
                Hashtable response = result.ToHashtable();
                this.UserKey = response["accesstoken"].ToString();
                this.UserKeyExpires = DateTime.Now + new TimeSpan(0, 0, response["expires"].ToInt());

                this.settings.SetString("RefreshToken", response["refreshtoken"].ToString());
                this.settings.SetString("UserName", response["user"].ToHashtable()["fullname"].ToString());
                this.settings.Save();

                this.LoadSettings();

                return true;
            } else {
                return false;
            }
        }

        public bool Login(string refreshtoken) {
            GettAPIRequest loginRequest = new GettAPIRequest(this, "users/login");
            loginRequest.APIKeyRequired = true;
            loginRequest.Add("refreshtoken", refreshtoken);
            return this.HandleLoginResponse(loginRequest.Execute());
        }

        public bool Login(string email, string password) {
            GettAPIRequest loginRequest = new GettAPIRequest(this, "users/login");
            loginRequest.APIKeyRequired = true;
            loginRequest.Add("email", email);
            loginRequest.Add("password", password);
            return this.HandleLoginResponse(loginRequest.Execute().ToHashtable());
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable UpItem = new Hashtable();
            UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList() || Clipboard.ContainsText());
            UpItem.Add("Text", "Ge.tt");
            UpItem.Add("Image", this.bmpIcon);
            UpItem.Add("Action", new Action(Upload));
            UpItem.Add("ShortcutModifiers", this.shortCutPasteModifiers);
            UpItem.Add("ShortcutKey", this.shortCutPasteKey);
            ret.Add(UpItem);

            return ret.ToArray();
        }

        public void Upload() {

        }

        public void Settings() {
            new FormSettings(this).ShowDialog();
        }
    }
}

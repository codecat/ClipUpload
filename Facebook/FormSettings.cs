using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facebook {
    public partial class FormSettings : Form {
        Facebook mainClass;

        public FormSettings(Facebook mainClass) {
            InitializeComponent();

            this.mainClass = mainClass;

            AuthedMessage();

            {
                string[] parts = mainClass.shortCutDragModifiers.Split('+');
                foreach (string part in parts) {
                    switch (part) {
                        case "Ctrl": checkDragModCtrl.Checked = true; break;
                        case "Alt": checkDragModAlt.Checked = true; break;
                        case "Shift": checkDragModShift.Checked = true; break;
                    }
                }
            }

            {
                string[] parts = mainClass.shortCutPasteModifiers.Split('+');
                foreach (string part in parts) {
                    switch (part) {
                        case "Ctrl": checkPasteModCtrl.Checked = true; break;
                        case "Alt": checkPasteModAlt.Checked = true; break;
                        case "Shift": checkPasteModShift.Checked = true; break;
                    }
                }
            }

            mainClass.PopulateKeysCombobox(comboDragKeys);
            mainClass.PopulateKeysCombobox(comboPasteKeys);

            comboDragKeys.SelectedItem = mainClass.shortCutDragKey;
            comboPasteKeys.SelectedItem = mainClass.shortCutPasteKey;
        }

        private void button1_Click(object sender, EventArgs e) {
            mainClass.settings.SetString("AccessToken", mainClass.facebookClient.AccessToken);

            {
                string shortcutModifiers = "";
                if (checkDragModCtrl.Checked) shortcutModifiers += "+Ctrl";
                if (checkDragModAlt.Checked) shortcutModifiers += "+Alt";
                if (checkDragModShift.Checked) shortcutModifiers += "+Shift";
                shortcutModifiers = shortcutModifiers.Trim('+');

                mainClass.settings.SetString("ShortcutDragModifiers", shortcutModifiers);
                mainClass.settings.SetString("ShortcutDragKey", (string)comboDragKeys.SelectedItem != "None" ? (string)comboDragKeys.SelectedItem : "");
            }

            {
                string shortcutModifiers = "";
                if (checkPasteModCtrl.Checked) shortcutModifiers += "+Ctrl";
                if (checkPasteModAlt.Checked) shortcutModifiers += "+Alt";
                if (checkPasteModShift.Checked) shortcutModifiers += "+Shift";
                shortcutModifiers = shortcutModifiers.Trim('+');

                mainClass.settings.SetString("ShortcutPasteModifiers", shortcutModifiers);
                mainClass.settings.SetString("ShortcutPasteKey", (string)comboPasteKeys.SelectedItem != "None" ? (string)comboPasteKeys.SelectedItem : "");
            }

            mainClass.settings.Save();

            mainClass.LoadSettings();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        public void AuthedMessage() {
            if (mainClass.facebookName != "") {
                this.labelAuthed.Text = "Authenticated as " + mainClass.facebookName + ".";
                this.button3.Text = "Deauthenticate";
            } else
                this.labelAuthed.Text = "Not authenticated.";
        }

        private void button3_Click(object sender, EventArgs e) {
            if (this.button3.Text == "Authenticate") {
                Dictionary<string, object> loginParams = new Dictionary<string, object>();
                loginParams["client_id"] = "294548907266945";
                loginParams["redirect_uri"] = "https://" + "www.facebook.com/connect/login_success.html";
                loginParams["display"] = "popup";
                loginParams["scope"] = "publish_stream";
                loginParams["response_type"] = "token";

                new FormAuthenticate(mainClass, mainClass.facebookClient.GetLoginUrl(loginParams).AbsoluteUri).ShowDialog();
            } else {
                mainClass.facebookClient.AccessToken = "";
                mainClass.facebookName = "";
            }

            AuthedMessage();
        }
    }
}

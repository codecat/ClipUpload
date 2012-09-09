using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTP {
    public partial class FormSettings : Form {
        FTP mainClass;

        public FormSettings(FTP mainClass) {
            InitializeComponent();

            this.mainClass = mainClass;

            textServer.Text = mainClass.ftpServer;
            textUsername.Text = mainClass.ftpUsername;
            textPassword.Text = mainClass.ftpPassword;
            textPath.Text = mainClass.ftpPath;
            checkPassive.Checked = mainClass.ftpPassive;
            checkBinary.Checked = mainClass.ftpBinary;
            textHttp.Text = mainClass.ftpHttp;

            int selIndex = 0;
            switch (mainClass.imageFormat.ToLower()) {
                case "png": selIndex = 0; break;
                case "jpg": selIndex = 1; break;
                case "gif": selIndex = 2; break;
            }
            comboFormat.SelectedIndex = selIndex;

            checkUseMD5.Checked = mainClass.useMD5;
            checkShortMD5.Checked = mainClass.shortMD5;

            numLength.Value = mainClass.length;

            checkJpegCompression.Checked = mainClass.jpegCompression;
            numJpegCompressionFilesize.Value = mainClass.jpegCompressionFilesize;
            numJpegCompressionRate.Value = mainClass.jpegCompressionRate;

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
            mainClass.settings.SetString("Server", textServer.Text);
            mainClass.settings.SetString("Username", textUsername.Text);
            mainClass.settings.SetString("Password", mainClass.base64Encode(textPassword.Text));
            mainClass.settings.SetString("Path", textPath.Text);
            mainClass.settings.SetBool("Passive", checkPassive.Checked);
            mainClass.settings.SetBool("Binary", checkBinary.Checked);
            mainClass.settings.SetString("Http", textHttp.Text);

            mainClass.settings.SetString("Format", comboFormat.Items[comboFormat.SelectedIndex].ToString());

            mainClass.settings.SetBool("UseMD5", checkUseMD5.Checked);
            mainClass.settings.SetBool("ShortMD5", checkShortMD5.Checked);

            mainClass.settings.SetInt("Length", (int)numLength.Value);

            mainClass.settings.SetBool("JpegCompression", checkJpegCompression.Checked);
            mainClass.settings.SetInt("JpegCompressionFilesize", (int)numJpegCompressionFilesize.Value);
            mainClass.settings.SetInt("JpegCompressionRate", (int)numJpegCompressionRate.Value);

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

        private void numShortMD5Count_ValueChanged(object sender, EventArgs e) {
            checkShortMD5.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e) {
            MessageBox.Show("This turns the uploaded image into a Jpeg instead of the usual format selected on the left. If the resulting filesize is larger than X amount of KB, it will use the given compression rate.", "Jpeg compression", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClipUpload3 {
    public partial class FormSettings : Form {
        FormMain mainClass;

        RegistryKey autostartRegKey;

        public FormSettings(FormMain mainClass) {
            this.mainClass = mainClass;

            InitializeComponent();

            autostartRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            checkAutostart.Checked = autostartRegKey.GetValue("ClipUpload3") != null;
            checkHideDonate.Checked = !mainClass.settings.GetBool("DonateVisible");

            checkProgressBar.Checked = mainClass.settings.GetBool("ProgressBar");
            checkPortableProgressbar.Checked = mainClass.settings.GetBool("PortableProgressBar");
            checkUpdates.Checked = mainClass.settings.GetBool("CheckForUpdates");

            checkUseProxy.Checked = mainClass.settings.GetBool("ProxyEnabled");
            textProxyHost.Text = mainClass.settings.GetString("ProxyHost");
            numProxyPort.Value = mainClass.settings.GetInt("ProxyPort");
            textProxyUsername.Text = mainClass.settings.GetString("ProxyUsername");
            textProxyPassword.Text = mainClass.settings.GetString("ProxyPassword");

            checkDragExtra.Checked = mainClass.settings.GetBool("DragExtra");
            textDragExtraName.Text = mainClass.settings.GetString("DragExtraName");
            textDragExtraPath.Text = mainClass.settings.GetString("DragExtraPath");
        }

        private void button2_Click(object sender, EventArgs e) {
            if (checkAutostart.Checked)
                autostartRegKey.SetValue("ClipUpload3", Application.ExecutablePath);
            else
                autostartRegKey.DeleteValue("ClipUpload3", false);

            mainClass.settings.SetBool("DonateVisible", !checkHideDonate.Checked);

            mainClass.settings.SetBool("ProgressBar", checkProgressBar.Checked);
            mainClass.settings.SetBool("PortableProgressBar", checkPortableProgressbar.Checked);
            mainClass.settings.SetBool("CheckForUpdates", checkUpdates.Checked);

            mainClass.settings.SetBool("ProxyEnabled", checkUseProxy.Checked);
            mainClass.settings.SetString("ProxyHost", textProxyHost.Text);
            mainClass.settings.SetInt("ProxyPort", (int)numProxyPort.Value);
            mainClass.settings.SetString("ProxyUsername", textProxyUsername.Text);
            mainClass.settings.SetString("ProxyPassword", textProxyPassword.Text);

            mainClass.settings.SetBool("DragExtra", checkDragExtra.Checked);
            mainClass.settings.SetString("DragExtraName", textDragExtraName.Text);
            mainClass.settings.SetString("DragExtraPath", textDragExtraPath.Text);

            mainClass.settings.Save();

            this.mainClass.LoadSettings();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            MessageBox.Show("If \"Edit process\" is enabled, it allows you to press the P key while dragging to open the current selection in an application such as MSPaint or any other image editing software. This way, you can edit your image there and remove unneeded parts or draw arrows around things that might be interesting. You can go full-out on these settings.", "ClipUpload 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Executables (*.exe)|*.exe|Batch scripts (*.bat)|*.bat|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK) {
                textDragExtraPath.Text = dialog.FileName;
            }
        }
    }
}

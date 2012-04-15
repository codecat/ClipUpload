using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Pastebin
{
    public partial class FormSettings : Form
    {
        Pastebin mainClass;

        public FormSettings(Pastebin mainClass)
        {
            InitializeComponent();

            this.mainClass = mainClass;

            labelStatus.Text = mainClass.UserLoggedIn ? "Logged in as " + mainClass.UserName + "." : "Not logged in.";
            buttonSignout.Enabled = mainClass.UserLoggedIn;
            checkShowPrivate.Checked = mainClass.ShowPrivate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainClass.settings.SetBool("ShowPrivate", checkShowPrivate.Checked);

            mainClass.settings.Save();

            mainClass.LoadSettings();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UsernamePasswordChange(object sender, EventArgs e)
        {
            buttonIdentify.Enabled = !mainClass.UserLoggedIn;
        }

        private void buttonIdentify_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string result = "Bad API request, no result";
            try
            {
                result = wc.UploadString("http://" + "pastebin.com/api/api_login.php", "api_dev_key=" + mainClass.PastebinAPIKey + "&api_user_name=" + Uri.EscapeDataString(textUsername.Text) + "&api_user_password=" + Uri.EscapeDataString(textPassword.Text));
            }
            catch { }

            if (!result.Contains("Bad API request, "))
            {
                if (result.Length == 32)
                {
                    mainClass.settings.SetString("UserKey", result);
                    mainClass.settings.SetString("UserName", textUsername.Text);
                    mainClass.settings.Save();

                    mainClass.UserLoggedIn = true;
                    mainClass.UserKey = result;

                    textUsername.Text = "";
                    textPassword.Text = "";
                    buttonIdentify.Enabled = false;
                }
                else
                    MessageBox.Show("Unexpected response: \"" + result + "\"");
            }
            else
                MessageBox.Show("Error: " + result.Split(new string[] { ", " }, StringSplitOptions.None)[1]);
        }

        private void buttonSignout_Click(object sender, EventArgs e)
        {
            mainClass.UserLoggedIn = false;
            mainClass.UserKey = "";
            mainClass.UserName = "";

            mainClass.settings.SetString("UserKey", "");
            mainClass.settings.SetString("UserName", "");

            labelStatus.Text = "Not logged in.";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;

namespace Gett {
    public partial class FormSettings : Form {
        Gett mainClass;

        public FormSettings(Gett mainClass) {
            InitializeComponent();

            this.mainClass = mainClass;

            labelStatus.Text = mainClass.UserLoggedIn ? "Logged in as " + mainClass.UserName + "." : "Not logged in.";
            buttonSignout.Enabled = mainClass.UserLoggedIn;
        }

        private void button1_Click(object sender, EventArgs e) {
            mainClass.settings.Save();

            mainClass.LoadSettings();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void UsernamePasswordChange(object sender, EventArgs e) {
            buttonIdentify.Enabled = !mainClass.UserLoggedIn;
        }

        private void buttonIdentify_Click(object sender, EventArgs e) {
            this.mainClass.Login(this.textUsername.Text, this.textPassword.Text);
        }

        private void buttonSignout_Click(object sender, EventArgs e) {
            mainClass.UserLoggedIn = false;
            mainClass.UserKey = "";
            mainClass.UserName = "";

            mainClass.settings.SetString("UserKey", "");
            mainClass.settings.SetString("UserName", "");

            labelStatus.Text = "Not logged in.";
        }
    }
}

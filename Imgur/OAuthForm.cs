using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MrAG.OAuth {
    public partial class OAuthForm : Form {
        private OAuth oauth;
        private string callbackURL;
        private bool usingURL;

        public string AccessToken;

        public OAuthForm(OAuth oauth, string callbackURL) {
            InitializeComponent();

            this.oauth = oauth;
            this.callbackURL = callbackURL;

            if (oauth.ServiceIcon != null)
                this.Icon = oauth.ServiceIcon;

            if (oauth.ServiceName != "")
                this.Text = "Authorize " + oauth.ServiceName;

            string authURL = oauth.GetAuthorizeURL(callbackURL);
            if (authURL != "")
                this.webAuth.Navigate(authURL);
            else
                this.webAuth.DocumentText = "<!DOCTYPE html><html><body><center><br/><br/><h1>" + this.oauth.ServiceName + " appears to be offline.</h1><p>Try again in a minute!</p><p><a href=\"" + authURL + "\">Retry now</a></p></center></body></html>";
        }

        private void webAuth_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
            if (this.usingURL) return;

            if (e.Url.AbsoluteUri.StartsWith(this.callbackURL)) {
                this.usingURL = true;

                this.webAuth.Visible = false;
                this.labelInfo.Visible = true;
                Application.DoEvents();

                if (this.oauth.GetAccessToken(e.Url.AbsoluteUri) == "")
                    MessageBox.Show("Something went wrong during the authentication. Please try again later.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
        }
    }
}

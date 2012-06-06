namespace MrAG.OAuth {
    partial class OAuthForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OAuthForm));
            this.webAuth = new System.Windows.Forms.WebBrowser();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webAuth
            // 
            this.webAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webAuth.Location = new System.Drawing.Point(0, 0);
            this.webAuth.MinimumSize = new System.Drawing.Size(20, 20);
            this.webAuth.Name = "webAuth";
            this.webAuth.Size = new System.Drawing.Size(926, 533);
            this.webAuth.TabIndex = 0;
            this.webAuth.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webAuth_Navigated);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI Semilight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(344, 37);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Authenticated! Please wait...";
            this.labelInfo.Visible = false;
            // 
            // OAuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 533);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.webAuth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OAuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webAuth;
        private System.Windows.Forms.Label labelInfo;
    }
}
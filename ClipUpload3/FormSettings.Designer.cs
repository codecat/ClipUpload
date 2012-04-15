namespace ClipUpload3
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkUpdates = new System.Windows.Forms.CheckBox();
            this.checkPortableProgressbar = new System.Windows.Forms.CheckBox();
            this.checkProgressBar = new System.Windows.Forms.CheckBox();
            this.checkHideDonate = new System.Windows.Forms.CheckBox();
            this.checkAutostart = new System.Windows.Forms.CheckBox();
            this.numProxyPort = new System.Windows.Forms.NumericUpDown();
            this.textProxyPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textProxyUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textProxyHost = new System.Windows.Forms.TextBox();
            this.checkUseProxy = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkDragExtra = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textDragExtraName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textDragExtraPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(253, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(172, 259);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkUpdates
            // 
            this.checkUpdates.AutoSize = true;
            this.checkUpdates.Location = new System.Drawing.Point(6, 98);
            this.checkUpdates.Name = "checkUpdates";
            this.checkUpdates.Size = new System.Drawing.Size(163, 17);
            this.checkUpdates.TabIndex = 2;
            this.checkUpdates.Text = "Check for updates on startup";
            this.checkUpdates.UseVisualStyleBackColor = true;
            // 
            // checkPortableProgressbar
            // 
            this.checkPortableProgressbar.AutoSize = true;
            this.checkPortableProgressbar.Location = new System.Drawing.Point(6, 75);
            this.checkPortableProgressbar.Name = "checkPortableProgressbar";
            this.checkPortableProgressbar.Size = new System.Drawing.Size(123, 17);
            this.checkPortableProgressbar.TabIndex = 1;
            this.checkPortableProgressbar.Text = "Portable progressbar";
            this.checkPortableProgressbar.UseVisualStyleBackColor = true;
            // 
            // checkProgressBar
            // 
            this.checkProgressBar.AutoSize = true;
            this.checkProgressBar.Location = new System.Drawing.Point(6, 52);
            this.checkProgressBar.Name = "checkProgressBar";
            this.checkProgressBar.Size = new System.Drawing.Size(111, 17);
            this.checkProgressBar.TabIndex = 1;
            this.checkProgressBar.Text = "Show progressbar";
            this.checkProgressBar.UseVisualStyleBackColor = true;
            // 
            // checkHideDonate
            // 
            this.checkHideDonate.AutoSize = true;
            this.checkHideDonate.Location = new System.Drawing.Point(6, 29);
            this.checkHideDonate.Name = "checkHideDonate";
            this.checkHideDonate.Size = new System.Drawing.Size(137, 17);
            this.checkHideDonate.TabIndex = 1;
            this.checkHideDonate.Text = "Hide the Donate button";
            this.checkHideDonate.UseVisualStyleBackColor = true;
            // 
            // checkAutostart
            // 
            this.checkAutostart.AutoSize = true;
            this.checkAutostart.Location = new System.Drawing.Point(6, 6);
            this.checkAutostart.Name = "checkAutostart";
            this.checkAutostart.Size = new System.Drawing.Size(117, 17);
            this.checkAutostart.TabIndex = 0;
            this.checkAutostart.Text = "Start with Windows";
            this.checkAutostart.UseVisualStyleBackColor = true;
            // 
            // numProxyPort
            // 
            this.numProxyPort.Location = new System.Drawing.Point(239, 29);
            this.numProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numProxyPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            this.numProxyPort.Size = new System.Drawing.Size(63, 20);
            this.numProxyPort.TabIndex = 4;
            this.numProxyPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // textProxyPassword
            // 
            this.textProxyPassword.Location = new System.Drawing.Point(70, 81);
            this.textProxyPassword.Name = "textProxyPassword";
            this.textProxyPassword.Size = new System.Drawing.Size(232, 20);
            this.textProxyPassword.TabIndex = 3;
            this.textProxyPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // textProxyUsername
            // 
            this.textProxyUsername.Location = new System.Drawing.Point(70, 55);
            this.textProxyUsername.Name = "textProxyUsername";
            this.textProxyUsername.Size = new System.Drawing.Size(232, 20);
            this.textProxyUsername.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Host:";
            // 
            // textProxyHost
            // 
            this.textProxyHost.Location = new System.Drawing.Point(70, 29);
            this.textProxyHost.Name = "textProxyHost";
            this.textProxyHost.Size = new System.Drawing.Size(163, 20);
            this.textProxyHost.TabIndex = 1;
            // 
            // checkUseProxy
            // 
            this.checkUseProxy.AutoSize = true;
            this.checkUseProxy.Location = new System.Drawing.Point(6, 6);
            this.checkUseProxy.Name = "checkUseProxy";
            this.checkUseProxy.Size = new System.Drawing.Size(59, 17);
            this.checkUseProxy.TabIndex = 0;
            this.checkUseProxy.Text = "Enable";
            this.checkUseProxy.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 241);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkUpdates);
            this.tabPage1.Controls.Add(this.checkAutostart);
            this.tabPage1.Controls.Add(this.checkPortableProgressbar);
            this.tabPage1.Controls.Add(this.checkHideDonate);
            this.tabPage1.Controls.Add(this.checkProgressBar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numProxyPort);
            this.tabPage2.Controls.Add(this.checkUseProxy);
            this.tabPage2.Controls.Add(this.textProxyPassword);
            this.tabPage2.Controls.Add(this.textProxyHost);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textProxyUsername);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(308, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.textDragExtraPath);
            this.tabPage3.Controls.Add(this.textDragExtraName);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.checkDragExtra);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(308, 215);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Drag Window";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkDragExtra
            // 
            this.checkDragExtra.AutoSize = true;
            this.checkDragExtra.Location = new System.Drawing.Point(6, 6);
            this.checkDragExtra.Name = "checkDragExtra";
            this.checkDragExtra.Size = new System.Drawing.Size(84, 17);
            this.checkDragExtra.TabIndex = 0;
            this.checkDragExtra.Text = "Edit process";
            this.checkDragExtra.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(275, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textDragExtraName
            // 
            this.textDragExtraName.Location = new System.Drawing.Point(75, 29);
            this.textDragExtraName.Name = "textDragExtraName";
            this.textDragExtraName.Size = new System.Drawing.Size(227, 20);
            this.textDragExtraName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Name:";
            // 
            // textDragExtraPath
            // 
            this.textDragExtraPath.Location = new System.Drawing.Point(75, 58);
            this.textDragExtraPath.Name = "textDragExtraPath";
            this.textDragExtraPath.Size = new System.Drawing.Size(194, 20);
            this.textDragExtraPath.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Executable:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(275, 56);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(27, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 294);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkAutostart;
        private System.Windows.Forms.CheckBox checkHideDonate;
        private System.Windows.Forms.CheckBox checkProgressBar;
        private System.Windows.Forms.CheckBox checkPortableProgressbar;
        private System.Windows.Forms.CheckBox checkUseProxy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textProxyHost;
        private System.Windows.Forms.TextBox textProxyPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textProxyUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkUpdates;
        private System.Windows.Forms.NumericUpDown numProxyPort;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkDragExtra;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textDragExtraName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDragExtraPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
    }
}
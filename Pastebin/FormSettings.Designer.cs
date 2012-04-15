namespace Pastebin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSignout = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonIdentify = new System.Windows.Forms.Button();
            this.checkShowPrivate = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSignout);
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.textPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textUsername);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonIdentify);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pastebin Account";
            // 
            // buttonSignout
            // 
            this.buttonSignout.Enabled = false;
            this.buttonSignout.Location = new System.Drawing.Point(137, 91);
            this.buttonSignout.Name = "buttonSignout";
            this.buttonSignout.Size = new System.Drawing.Size(75, 23);
            this.buttonSignout.TabIndex = 2;
            this.buttonSignout.Text = "Sign out";
            this.buttonSignout.UseVisualStyleBackColor = true;
            this.buttonSignout.Click += new System.EventHandler(this.buttonSignout_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(84, 68);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(209, 20);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Not logged in.";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(84, 45);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(209, 20);
            this.textPassword.TabIndex = 1;
            this.textPassword.UseSystemPasswordChar = true;
            this.textPassword.TextChanged += new System.EventHandler(this.UsernamePasswordChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(84, 19);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(209, 20);
            this.textUsername.TabIndex = 0;
            this.textUsername.TextChanged += new System.EventHandler(this.UsernamePasswordChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // buttonIdentify
            // 
            this.buttonIdentify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIdentify.Enabled = false;
            this.buttonIdentify.Location = new System.Drawing.Point(218, 91);
            this.buttonIdentify.Name = "buttonIdentify";
            this.buttonIdentify.Size = new System.Drawing.Size(75, 23);
            this.buttonIdentify.TabIndex = 3;
            this.buttonIdentify.Text = "Identify";
            this.buttonIdentify.UseVisualStyleBackColor = true;
            this.buttonIdentify.Click += new System.EventHandler(this.buttonIdentify_Click);
            // 
            // checkShowPrivate
            // 
            this.checkShowPrivate.AutoSize = true;
            this.checkShowPrivate.Location = new System.Drawing.Point(12, 138);
            this.checkShowPrivate.Name = "checkShowPrivate";
            this.checkShowPrivate.Size = new System.Drawing.Size(125, 17);
            this.checkShowPrivate.TabIndex = 6;
            this.checkShowPrivate.Text = "Show private options";
            this.checkShowPrivate.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 345);
            this.Controls.Add(this.checkShowPrivate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pastebin Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonIdentify;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonSignout;
        private System.Windows.Forms.CheckBox checkShowPrivate;
    }
}
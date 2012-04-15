namespace FTP
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
            this.label7 = new System.Windows.Forms.Label();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.checkShortMD5 = new System.Windows.Forms.CheckBox();
            this.checkUseMD5 = new System.Windows.Forms.CheckBox();
            this.textHttp = new System.Windows.Forms.TextBox();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkPassive = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkDragModCtrl = new System.Windows.Forms.CheckBox();
            this.checkDragModAlt = new System.Windows.Forms.CheckBox();
            this.checkDragModShift = new System.Windows.Forms.CheckBox();
            this.comboDragKeys = new System.Windows.Forms.ComboBox();
            this.checkBinary = new System.Windows.Forms.CheckBox();
            this.comboPasteKeys = new System.Windows.Forms.ComboBox();
            this.checkPasteModShift = new System.Windows.Forms.CheckBox();
            this.checkPasteModAlt = new System.Windows.Forms.CheckBox();
            this.checkPasteModCtrl = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 387);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Length:";
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(92, 261);
            this.numLength.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(71, 20);
            this.numLength.TabIndex = 10;
            this.numLength.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // checkShortMD5
            // 
            this.checkShortMD5.AutoSize = true;
            this.checkShortMD5.Location = new System.Drawing.Point(92, 238);
            this.checkShortMD5.Name = "checkShortMD5";
            this.checkShortMD5.Size = new System.Drawing.Size(77, 17);
            this.checkShortMD5.TabIndex = 9;
            this.checkShortMD5.Text = "Short MD5";
            this.checkShortMD5.UseVisualStyleBackColor = true;
            // 
            // checkUseMD5
            // 
            this.checkUseMD5.AutoSize = true;
            this.checkUseMD5.Location = new System.Drawing.Point(92, 215);
            this.checkUseMD5.Name = "checkUseMD5";
            this.checkUseMD5.Size = new System.Drawing.Size(71, 17);
            this.checkUseMD5.TabIndex = 8;
            this.checkUseMD5.Text = "Use MD5";
            this.checkUseMD5.UseVisualStyleBackColor = true;
            // 
            // textHttp
            // 
            this.textHttp.Location = new System.Drawing.Point(92, 162);
            this.textHttp.Name = "textHttp";
            this.textHttp.Size = new System.Drawing.Size(219, 20);
            this.textHttp.TabIndex = 6;
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(92, 90);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(219, 20);
            this.textPath.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Http:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Path:";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(92, 64);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(219, 20);
            this.textPassword.TabIndex = 2;
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password:";
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(92, 38);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(219, 20);
            this.textUsername.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Username:";
            // 
            // textServer
            // 
            this.textServer.Location = new System.Drawing.Point(92, 12);
            this.textServer.Name = "textServer";
            this.textServer.Size = new System.Drawing.Size(219, 20);
            this.textServer.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Server:";
            // 
            // comboFormat
            // 
            this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFormat.FormattingEnabled = true;
            this.comboFormat.Items.AddRange(new object[] {
            "PNG",
            "JPG",
            "GIF"});
            this.comboFormat.Location = new System.Drawing.Point(92, 188);
            this.comboFormat.Name = "comboFormat";
            this.comboFormat.Size = new System.Drawing.Size(136, 21);
            this.comboFormat.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Format:";
            // 
            // checkPassive
            // 
            this.checkPassive.AutoSize = true;
            this.checkPassive.Location = new System.Drawing.Point(92, 116);
            this.checkPassive.Name = "checkPassive";
            this.checkPassive.Size = new System.Drawing.Size(92, 17);
            this.checkPassive.TabIndex = 4;
            this.checkPassive.Text = "Passive mode";
            this.checkPassive.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 287);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Drag shortcut:";
            // 
            // checkDragModCtrl
            // 
            this.checkDragModCtrl.AutoSize = true;
            this.checkDragModCtrl.Location = new System.Drawing.Point(92, 287);
            this.checkDragModCtrl.Name = "checkDragModCtrl";
            this.checkDragModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkDragModCtrl.TabIndex = 11;
            this.checkDragModCtrl.Text = "Ctrl";
            this.checkDragModCtrl.UseVisualStyleBackColor = true;
            // 
            // checkDragModAlt
            // 
            this.checkDragModAlt.AutoSize = true;
            this.checkDragModAlt.Location = new System.Drawing.Point(139, 287);
            this.checkDragModAlt.Name = "checkDragModAlt";
            this.checkDragModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkDragModAlt.TabIndex = 12;
            this.checkDragModAlt.Text = "Alt";
            this.checkDragModAlt.UseVisualStyleBackColor = true;
            // 
            // checkDragModShift
            // 
            this.checkDragModShift.AutoSize = true;
            this.checkDragModShift.Location = new System.Drawing.Point(183, 287);
            this.checkDragModShift.Name = "checkDragModShift";
            this.checkDragModShift.Size = new System.Drawing.Size(47, 17);
            this.checkDragModShift.TabIndex = 13;
            this.checkDragModShift.Text = "Shift";
            this.checkDragModShift.UseVisualStyleBackColor = true;
            // 
            // comboDragKeys
            // 
            this.comboDragKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDragKeys.FormattingEnabled = true;
            this.comboDragKeys.Location = new System.Drawing.Point(92, 310);
            this.comboDragKeys.Name = "comboDragKeys";
            this.comboDragKeys.Size = new System.Drawing.Size(136, 21);
            this.comboDragKeys.TabIndex = 14;
            // 
            // checkBinary
            // 
            this.checkBinary.AutoSize = true;
            this.checkBinary.Location = new System.Drawing.Point(92, 139);
            this.checkBinary.Name = "checkBinary";
            this.checkBinary.Size = new System.Drawing.Size(84, 17);
            this.checkBinary.TabIndex = 5;
            this.checkBinary.Text = "Binary mode";
            this.checkBinary.UseVisualStyleBackColor = true;
            // 
            // comboPasteKeys
            // 
            this.comboPasteKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPasteKeys.FormattingEnabled = true;
            this.comboPasteKeys.Location = new System.Drawing.Point(92, 360);
            this.comboPasteKeys.Name = "comboPasteKeys";
            this.comboPasteKeys.Size = new System.Drawing.Size(136, 21);
            this.comboPasteKeys.TabIndex = 18;
            // 
            // checkPasteModShift
            // 
            this.checkPasteModShift.AutoSize = true;
            this.checkPasteModShift.Location = new System.Drawing.Point(183, 337);
            this.checkPasteModShift.Name = "checkPasteModShift";
            this.checkPasteModShift.Size = new System.Drawing.Size(47, 17);
            this.checkPasteModShift.TabIndex = 17;
            this.checkPasteModShift.Text = "Shift";
            this.checkPasteModShift.UseVisualStyleBackColor = true;
            // 
            // checkPasteModAlt
            // 
            this.checkPasteModAlt.AutoSize = true;
            this.checkPasteModAlt.Location = new System.Drawing.Point(139, 337);
            this.checkPasteModAlt.Name = "checkPasteModAlt";
            this.checkPasteModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkPasteModAlt.TabIndex = 16;
            this.checkPasteModAlt.Text = "Alt";
            this.checkPasteModAlt.UseVisualStyleBackColor = true;
            // 
            // checkPasteModCtrl
            // 
            this.checkPasteModCtrl.AutoSize = true;
            this.checkPasteModCtrl.Location = new System.Drawing.Point(92, 337);
            this.checkPasteModCtrl.Name = "checkPasteModCtrl";
            this.checkPasteModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkPasteModCtrl.TabIndex = 15;
            this.checkPasteModCtrl.Text = "Ctrl";
            this.checkPasteModCtrl.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 337);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Paste shortcut:";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 421);
            this.Controls.Add(this.comboPasteKeys);
            this.Controls.Add(this.checkPasteModShift);
            this.Controls.Add(this.checkPasteModAlt);
            this.Controls.Add(this.checkPasteModCtrl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboDragKeys);
            this.Controls.Add(this.checkDragModShift);
            this.Controls.Add(this.checkDragModAlt);
            this.Controls.Add(this.checkDragModCtrl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBinary);
            this.Controls.Add(this.checkPassive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.checkShortMD5);
            this.Controls.Add(this.checkUseMD5);
            this.Controls.Add(this.textHttp);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.CheckBox checkShortMD5;
        private System.Windows.Forms.CheckBox checkUseMD5;
        private System.Windows.Forms.TextBox textHttp;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkPassive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkDragModCtrl;
        private System.Windows.Forms.CheckBox checkDragModAlt;
        private System.Windows.Forms.CheckBox checkDragModShift;
        private System.Windows.Forms.ComboBox comboDragKeys;
        private System.Windows.Forms.CheckBox checkBinary;
        private System.Windows.Forms.ComboBox comboPasteKeys;
        private System.Windows.Forms.CheckBox checkPasteModShift;
        private System.Windows.Forms.CheckBox checkPasteModAlt;
        private System.Windows.Forms.CheckBox checkPasteModCtrl;
        private System.Windows.Forms.Label label9;
    }
}
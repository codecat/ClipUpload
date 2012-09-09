namespace S3
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
            this.prefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AWSSecretKet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AWSKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bucket = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkPrivate = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkDragModCtrl = new System.Windows.Forms.CheckBox();
            this.checkDragModAlt = new System.Windows.Forms.CheckBox();
            this.checkDragModShift = new System.Windows.Forms.CheckBox();
            this.comboDragKeys = new System.Windows.Forms.ComboBox();
            this.comboPasteKeys = new System.Windows.Forms.ComboBox();
            this.checkPasteModShift = new System.Windows.Forms.CheckBox();
            this.checkPasteModAlt = new System.Windows.Forms.CheckBox();
            this.checkPasteModCtrl = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.expires = new System.Windows.Forms.NumericUpDown();
            this.shorten = new System.Windows.Forms.CheckBox();
            this.https = new System.Windows.Forms.CheckBox();
            this.appendext = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.expires)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 347);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // prefix
            // 
            this.prefix.Location = new System.Drawing.Point(92, 93);
            this.prefix.Name = "prefix";
            this.prefix.Size = new System.Drawing.Size(219, 20);
            this.prefix.TabIndex = 3;
            this.prefix.TextChanged += new System.EventHandler(this.textPath_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Expires in Days:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Prefix:";
            // 
            // AWSSecretKet
            // 
            this.AWSSecretKet.Location = new System.Drawing.Point(92, 64);
            this.AWSSecretKet.Name = "AWSSecretKet";
            this.AWSSecretKet.Size = new System.Drawing.Size(219, 20);
            this.AWSSecretKet.TabIndex = 2;
            this.AWSSecretKet.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Secret Key:";
            // 
            // AWSKey
            // 
            this.AWSKey.Location = new System.Drawing.Point(92, 38);
            this.AWSKey.Name = "AWSKey";
            this.AWSKey.Size = new System.Drawing.Size(219, 20);
            this.AWSKey.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "AWS Key:";
            // 
            // bucket
            // 
            this.bucket.Location = new System.Drawing.Point(92, 12);
            this.bucket.Name = "bucket";
            this.bucket.Size = new System.Drawing.Size(219, 20);
            this.bucket.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Bucket Name:";
            // 
            // comboFormat
            // 
            this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFormat.FormattingEnabled = true;
            this.comboFormat.Items.AddRange(new object[] {
            "PNG",
            "JPG",
            "GIF"});
            this.comboFormat.Location = new System.Drawing.Point(92, 214);
            this.comboFormat.Name = "comboFormat";
            this.comboFormat.Size = new System.Drawing.Size(136, 21);
            this.comboFormat.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Format:";
            // 
            // checkPrivate
            // 
            this.checkPrivate.AutoSize = true;
            this.checkPrivate.Location = new System.Drawing.Point(177, 124);
            this.checkPrivate.Name = "checkPrivate";
            this.checkPrivate.Size = new System.Drawing.Size(59, 17);
            this.checkPrivate.TabIndex = 4;
            this.checkPrivate.Text = "Private";
            this.checkPrivate.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Drag shortcut:";
            // 
            // checkDragModCtrl
            // 
            this.checkDragModCtrl.AutoSize = true;
            this.checkDragModCtrl.Location = new System.Drawing.Point(92, 247);
            this.checkDragModCtrl.Name = "checkDragModCtrl";
            this.checkDragModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkDragModCtrl.TabIndex = 11;
            this.checkDragModCtrl.Text = "Ctrl";
            this.checkDragModCtrl.UseVisualStyleBackColor = true;
            // 
            // checkDragModAlt
            // 
            this.checkDragModAlt.AutoSize = true;
            this.checkDragModAlt.Location = new System.Drawing.Point(139, 247);
            this.checkDragModAlt.Name = "checkDragModAlt";
            this.checkDragModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkDragModAlt.TabIndex = 12;
            this.checkDragModAlt.Text = "Alt";
            this.checkDragModAlt.UseVisualStyleBackColor = true;
            // 
            // checkDragModShift
            // 
            this.checkDragModShift.AutoSize = true;
            this.checkDragModShift.Location = new System.Drawing.Point(183, 247);
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
            this.comboDragKeys.Location = new System.Drawing.Point(92, 270);
            this.comboDragKeys.Name = "comboDragKeys";
            this.comboDragKeys.Size = new System.Drawing.Size(136, 21);
            this.comboDragKeys.TabIndex = 14;
            // 
            // comboPasteKeys
            // 
            this.comboPasteKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPasteKeys.FormattingEnabled = true;
            this.comboPasteKeys.Location = new System.Drawing.Point(92, 320);
            this.comboPasteKeys.Name = "comboPasteKeys";
            this.comboPasteKeys.Size = new System.Drawing.Size(136, 21);
            this.comboPasteKeys.TabIndex = 18;
            // 
            // checkPasteModShift
            // 
            this.checkPasteModShift.AutoSize = true;
            this.checkPasteModShift.Location = new System.Drawing.Point(183, 297);
            this.checkPasteModShift.Name = "checkPasteModShift";
            this.checkPasteModShift.Size = new System.Drawing.Size(47, 17);
            this.checkPasteModShift.TabIndex = 17;
            this.checkPasteModShift.Text = "Shift";
            this.checkPasteModShift.UseVisualStyleBackColor = true;
            // 
            // checkPasteModAlt
            // 
            this.checkPasteModAlt.AutoSize = true;
            this.checkPasteModAlt.Location = new System.Drawing.Point(139, 297);
            this.checkPasteModAlt.Name = "checkPasteModAlt";
            this.checkPasteModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkPasteModAlt.TabIndex = 16;
            this.checkPasteModAlt.Text = "Alt";
            this.checkPasteModAlt.UseVisualStyleBackColor = true;
            // 
            // checkPasteModCtrl
            // 
            this.checkPasteModCtrl.AutoSize = true;
            this.checkPasteModCtrl.Location = new System.Drawing.Point(92, 297);
            this.checkPasteModCtrl.Name = "checkPasteModCtrl";
            this.checkPasteModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkPasteModCtrl.TabIndex = 15;
            this.checkPasteModCtrl.Text = "Ctrl";
            this.checkPasteModCtrl.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 297);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Paste shortcut:";
            // 
            // expires
            // 
            this.expires.Location = new System.Drawing.Point(92, 121);
            this.expires.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.expires.Name = "expires";
            this.expires.Size = new System.Drawing.Size(71, 20);
            this.expires.TabIndex = 31;
            this.expires.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // shorten
            // 
            this.shorten.AutoSize = true;
            this.shorten.Location = new System.Drawing.Point(95, 176);
            this.shorten.Name = "shorten";
            this.shorten.Size = new System.Drawing.Size(86, 17);
            this.shorten.TabIndex = 32;
            this.shorten.Text = "URL shorten";
            this.shorten.UseVisualStyleBackColor = true;
            this.shorten.CheckedChanged += new System.EventHandler(this.shorten_CheckedChanged);
            // 
            // https
            // 
            this.https.AutoSize = true;
            this.https.Location = new System.Drawing.Point(246, 124);
            this.https.Name = "https";
            this.https.Size = new System.Drawing.Size(62, 17);
            this.https.TabIndex = 33;
            this.https.Text = "HTTPS";
            this.https.UseVisualStyleBackColor = true;
            // 
            // appendext
            // 
            this.appendext.AutoSize = true;
            this.appendext.Location = new System.Drawing.Point(194, 176);
            this.appendext.Name = "appendext";
            this.appendext.Size = new System.Drawing.Size(80, 17);
            this.appendext.TabIndex = 35;
            this.appendext.Text = "Append ext";
            this.appendext.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(6, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 45);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "is.gd Shortener";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 385);
            this.Controls.Add(this.appendext);
            this.Controls.Add(this.https);
            this.Controls.Add(this.shorten);
            this.Controls.Add(this.expires);
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
            this.Controls.Add(this.checkPrivate);
            this.Controls.Add(this.prefix);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AWSSecretKet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AWSKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bucket);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Amazon S3 Settings";
            ((System.ComponentModel.ISupportInitialize)(this.expires)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox prefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AWSSecretKet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AWSKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bucket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkPrivate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkDragModCtrl;
        private System.Windows.Forms.CheckBox checkDragModAlt;
        private System.Windows.Forms.CheckBox checkDragModShift;
        private System.Windows.Forms.ComboBox comboDragKeys;
        private System.Windows.Forms.ComboBox comboPasteKeys;
        private System.Windows.Forms.CheckBox checkPasteModShift;
        private System.Windows.Forms.CheckBox checkPasteModAlt;
        private System.Windows.Forms.CheckBox checkPasteModCtrl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown expires;
        private System.Windows.Forms.CheckBox shorten;
        private System.Windows.Forms.CheckBox https;
        private System.Windows.Forms.CheckBox appendext;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
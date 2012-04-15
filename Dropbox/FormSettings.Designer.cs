namespace Dropbox
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textHttp = new System.Windows.Forms.TextBox();
            this.checkUseMD5 = new System.Windows.Forms.CheckBox();
            this.checkShortMD5 = new System.Windows.Forms.CheckBox();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupNoDropbox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboPasteKeys = new System.Windows.Forms.ComboBox();
            this.checkPasteModShift = new System.Windows.Forms.CheckBox();
            this.checkPasteModAlt = new System.Windows.Forms.CheckBox();
            this.checkPasteModCtrl = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboDragKeys = new System.Windows.Forms.ComboBox();
            this.checkDragModShift = new System.Windows.Forms.CheckBox();
            this.checkDragModAlt = new System.Windows.Forms.CheckBox();
            this.checkDragModCtrl = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.groupNoDropbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image format:";
            // 
            // comboFormat
            // 
            this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFormat.FormattingEnabled = true;
            this.comboFormat.Items.AddRange(new object[] {
            "PNG",
            "JPG",
            "GIF"});
            this.comboFormat.Location = new System.Drawing.Point(92, 64);
            this.comboFormat.Name = "comboFormat";
            this.comboFormat.Size = new System.Drawing.Size(162, 21);
            this.comboFormat.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Path:";
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(92, 12);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(219, 20);
            this.textPath.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Http:";
            // 
            // textHttp
            // 
            this.textHttp.Location = new System.Drawing.Point(92, 38);
            this.textHttp.Name = "textHttp";
            this.textHttp.Size = new System.Drawing.Size(185, 20);
            this.textHttp.TabIndex = 1;
            // 
            // checkUseMD5
            // 
            this.checkUseMD5.AutoSize = true;
            this.checkUseMD5.Location = new System.Drawing.Point(92, 91);
            this.checkUseMD5.Name = "checkUseMD5";
            this.checkUseMD5.Size = new System.Drawing.Size(71, 17);
            this.checkUseMD5.TabIndex = 3;
            this.checkUseMD5.Text = "Use MD5";
            this.checkUseMD5.UseVisualStyleBackColor = true;
            // 
            // checkShortMD5
            // 
            this.checkShortMD5.AutoSize = true;
            this.checkShortMD5.Location = new System.Drawing.Point(92, 114);
            this.checkShortMD5.Name = "checkShortMD5";
            this.checkShortMD5.Size = new System.Drawing.Size(77, 17);
            this.checkShortMD5.TabIndex = 4;
            this.checkShortMD5.Text = "Short MD5";
            this.checkShortMD5.UseVisualStyleBackColor = true;
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(92, 137);
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
            this.numLength.TabIndex = 5;
            this.numLength.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numLength.ValueChanged += new System.EventHandler(this.numShortMD5Count_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Length:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(283, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 20);
            this.button3.TabIndex = 8;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupNoDropbox
            // 
            this.groupNoDropbox.Controls.Add(this.label2);
            this.groupNoDropbox.Location = new System.Drawing.Point(12, 263);
            this.groupNoDropbox.Name = "groupNoDropbox";
            this.groupNoDropbox.Size = new System.Drawing.Size(299, 76);
            this.groupNoDropbox.TabIndex = 9;
            this.groupNoDropbox.TabStop = false;
            this.groupNoDropbox.Text = "NOTE!";
            this.groupNoDropbox.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 57);
            this.label2.TabIndex = 0;
            this.label2.Text = "You don\'t have Dropbox installed on your PC!\r\nYou cannot use this addon without D" +
    "ropbox installed.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboPasteKeys
            // 
            this.comboPasteKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPasteKeys.FormattingEnabled = true;
            this.comboPasteKeys.Location = new System.Drawing.Point(92, 236);
            this.comboPasteKeys.Name = "comboPasteKeys";
            this.comboPasteKeys.Size = new System.Drawing.Size(136, 21);
            this.comboPasteKeys.TabIndex = 38;
            // 
            // checkPasteModShift
            // 
            this.checkPasteModShift.AutoSize = true;
            this.checkPasteModShift.Location = new System.Drawing.Point(183, 213);
            this.checkPasteModShift.Name = "checkPasteModShift";
            this.checkPasteModShift.Size = new System.Drawing.Size(47, 17);
            this.checkPasteModShift.TabIndex = 37;
            this.checkPasteModShift.Text = "Shift";
            this.checkPasteModShift.UseVisualStyleBackColor = true;
            // 
            // checkPasteModAlt
            // 
            this.checkPasteModAlt.AutoSize = true;
            this.checkPasteModAlt.Location = new System.Drawing.Point(139, 213);
            this.checkPasteModAlt.Name = "checkPasteModAlt";
            this.checkPasteModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkPasteModAlt.TabIndex = 36;
            this.checkPasteModAlt.Text = "Alt";
            this.checkPasteModAlt.UseVisualStyleBackColor = true;
            // 
            // checkPasteModCtrl
            // 
            this.checkPasteModCtrl.AutoSize = true;
            this.checkPasteModCtrl.Location = new System.Drawing.Point(92, 213);
            this.checkPasteModCtrl.Name = "checkPasteModCtrl";
            this.checkPasteModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkPasteModCtrl.TabIndex = 35;
            this.checkPasteModCtrl.Text = "Ctrl";
            this.checkPasteModCtrl.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "Paste shortcut:";
            // 
            // comboDragKeys
            // 
            this.comboDragKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDragKeys.FormattingEnabled = true;
            this.comboDragKeys.Location = new System.Drawing.Point(92, 186);
            this.comboDragKeys.Name = "comboDragKeys";
            this.comboDragKeys.Size = new System.Drawing.Size(136, 21);
            this.comboDragKeys.TabIndex = 34;
            // 
            // checkDragModShift
            // 
            this.checkDragModShift.AutoSize = true;
            this.checkDragModShift.Location = new System.Drawing.Point(183, 163);
            this.checkDragModShift.Name = "checkDragModShift";
            this.checkDragModShift.Size = new System.Drawing.Size(47, 17);
            this.checkDragModShift.TabIndex = 33;
            this.checkDragModShift.Text = "Shift";
            this.checkDragModShift.UseVisualStyleBackColor = true;
            // 
            // checkDragModAlt
            // 
            this.checkDragModAlt.AutoSize = true;
            this.checkDragModAlt.Location = new System.Drawing.Point(139, 163);
            this.checkDragModAlt.Name = "checkDragModAlt";
            this.checkDragModAlt.Size = new System.Drawing.Size(38, 17);
            this.checkDragModAlt.TabIndex = 32;
            this.checkDragModAlt.Text = "Alt";
            this.checkDragModAlt.UseVisualStyleBackColor = true;
            // 
            // checkDragModCtrl
            // 
            this.checkDragModCtrl.AutoSize = true;
            this.checkDragModCtrl.Location = new System.Drawing.Point(92, 163);
            this.checkDragModCtrl.Name = "checkDragModCtrl";
            this.checkDragModCtrl.Size = new System.Drawing.Size(41, 17);
            this.checkDragModCtrl.TabIndex = 31;
            this.checkDragModCtrl.Text = "Ctrl";
            this.checkDragModCtrl.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Drag shortcut:";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 380);
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
            this.Controls.Add(this.groupNoDropbox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.checkShortMD5);
            this.Controls.Add(this.checkUseMD5);
            this.Controls.Add(this.textHttp);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dropbox Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.groupNoDropbox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textHttp;
        private System.Windows.Forms.CheckBox checkUseMD5;
        private System.Windows.Forms.CheckBox checkShortMD5;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupNoDropbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboPasteKeys;
        private System.Windows.Forms.CheckBox checkPasteModShift;
        private System.Windows.Forms.CheckBox checkPasteModAlt;
        private System.Windows.Forms.CheckBox checkPasteModCtrl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboDragKeys;
        private System.Windows.Forms.CheckBox checkDragModShift;
        private System.Windows.Forms.CheckBox checkDragModAlt;
        private System.Windows.Forms.CheckBox checkDragModCtrl;
        private System.Windows.Forms.Label label8;
    }
}
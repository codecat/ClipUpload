namespace AddonHelper {
    partial class FormEditor {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonAccept = new System.Windows.Forms.ToolStripButton();
            this.buttonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCover = new System.Windows.Forms.ToolStripButton();
            this.buttonText = new System.Windows.Forms.ToolStripButton();
            this.buttonDraw = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPickColor = new System.Windows.Forms.ToolStripButton();
            this.buttonFont = new System.Windows.Forms.ToolStripButton();
            this.Stage = new System.Windows.Forms.PictureBox();
            this.timerSet = new System.Windows.Forms.Timer(this.components);
            this.boxPreview = new System.Windows.Forms.Label();
            this.textPreview = new System.Windows.Forms.TextBox();
            this.buttonCrop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAppend = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stage)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAccept,
            this.buttonCancel,
            this.toolStripSeparator1,
            this.buttonCover,
            this.buttonCrop,
            this.buttonText,
            this.buttonDraw,
            this.toolStripSeparator2,
            this.buttonAppend,
            this.toolStripSeparator3,
            this.buttonPickColor,
            this.buttonFont});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(684, 25);
            this.toolStrip.TabIndex = 0;
            // 
            // buttonAccept
            // 
            this.buttonAccept.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAccept.Image = ((System.Drawing.Image)(resources.GetObject("buttonAccept.Image")));
            this.buttonAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(23, 22);
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(23, 22);
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonCover
            // 
            this.buttonCover.CheckOnClick = true;
            this.buttonCover.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCover.Image = ((System.Drawing.Image)(resources.GetObject("buttonCover.Image")));
            this.buttonCover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCover.Name = "buttonCover";
            this.buttonCover.Size = new System.Drawing.Size(23, 22);
            this.buttonCover.Text = "Cover";
            this.buttonCover.Click += new System.EventHandler(this.buttonCover_Click);
            // 
            // buttonText
            // 
            this.buttonText.CheckOnClick = true;
            this.buttonText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonText.Image = ((System.Drawing.Image)(resources.GetObject("buttonText.Image")));
            this.buttonText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(23, 22);
            this.buttonText.Text = "Text";
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            // 
            // buttonDraw
            // 
            this.buttonDraw.CheckOnClick = true;
            this.buttonDraw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDraw.Image = ((System.Drawing.Image)(resources.GetObject("buttonDraw.Image")));
            this.buttonDraw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(23, 22);
            this.buttonDraw.Text = "Draw";
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonPickColor
            // 
            this.buttonPickColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPickColor.Image = ((System.Drawing.Image)(resources.GetObject("buttonPickColor.Image")));
            this.buttonPickColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPickColor.Name = "buttonPickColor";
            this.buttonPickColor.Size = new System.Drawing.Size(23, 22);
            this.buttonPickColor.Text = "Pick color";
            this.buttonPickColor.Click += new System.EventHandler(this.buttonPickColor_Click);
            // 
            // buttonFont
            // 
            this.buttonFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFont.Image = ((System.Drawing.Image)(resources.GetObject("buttonFont.Image")));
            this.buttonFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(23, 22);
            this.buttonFont.Text = "Font";
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // Stage
            // 
            this.Stage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Stage.Location = new System.Drawing.Point(0, 25);
            this.Stage.Name = "Stage";
            this.Stage.Size = new System.Drawing.Size(684, 540);
            this.Stage.TabIndex = 1;
            this.Stage.TabStop = false;
            this.Stage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Stage_MouseDown);
            this.Stage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Stage_MouseMove);
            this.Stage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Stage_MouseUp);
            // 
            // timerSet
            // 
            this.timerSet.Interval = 17;
            this.timerSet.Tick += new System.EventHandler(this.timerSet_Tick);
            // 
            // boxPreview
            // 
            this.boxPreview.BackColor = System.Drawing.Color.Maroon;
            this.boxPreview.Location = new System.Drawing.Point(599, 43);
            this.boxPreview.Name = "boxPreview";
            this.boxPreview.Size = new System.Drawing.Size(73, 79);
            this.boxPreview.TabIndex = 2;
            this.boxPreview.Visible = false;
            // 
            // textPreview
            // 
            this.textPreview.Location = new System.Drawing.Point(308, 43);
            this.textPreview.Name = "textPreview";
            this.textPreview.Size = new System.Drawing.Size(285, 20);
            this.textPreview.TabIndex = 3;
            this.textPreview.Visible = false;
            this.textPreview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPreview_KeyDown);
            // 
            // buttonCrop
            // 
            this.buttonCrop.CheckOnClick = true;
            this.buttonCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCrop.Image = ((System.Drawing.Image)(resources.GetObject("buttonCrop.Image")));
            this.buttonCrop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCrop.Name = "buttonCrop";
            this.buttonCrop.Size = new System.Drawing.Size(23, 22);
            this.buttonCrop.Text = "Crop";
            this.buttonCrop.Click += new System.EventHandler(this.buttonCrop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonAppend
            // 
            this.buttonAppend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAppend.Image = ((System.Drawing.Image)(resources.GetObject("buttonAppend.Image")));
            this.buttonAppend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAppend.Name = "buttonAppend";
            this.buttonAppend.Size = new System.Drawing.Size(23, 22);
            this.buttonAppend.Text = "Drag -> Append Image";
            this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 565);
            this.Controls.Add(this.textPreview);
            this.Controls.Add(this.boxPreview);
            this.Controls.Add(this.Stage);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image editor";
            this.Load += new System.EventHandler(this.FormEditor_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Stage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonAccept;
        private System.Windows.Forms.ToolStripButton buttonCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonCover;
        private System.Windows.Forms.PictureBox Stage;
        private System.Windows.Forms.Timer timerSet;
        private System.Windows.Forms.Label boxPreview;
        private System.Windows.Forms.ToolStripButton buttonPickColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonText;
        private System.Windows.Forms.ToolStripButton buttonDraw;
        private System.Windows.Forms.TextBox textPreview;
        private System.Windows.Forms.ToolStripButton buttonFont;
        private System.Windows.Forms.ToolStripButton buttonCrop;
        private System.Windows.Forms.ToolStripButton buttonAppend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
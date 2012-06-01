namespace AddonHelper
{
    partial class FormDrag
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
            this.components = new System.ComponentModel.Container();
            this.ScreenBox = new System.Windows.Forms.Label();
            this.timerSet = new System.Windows.Forms.Timer(this.components);
            this.picHeight = new System.Windows.Forms.PictureBox();
            this.picWidth = new System.Windows.Forms.PictureBox();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.labelExtraInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenBox
            // 
            this.ScreenBox.BackColor = System.Drawing.Color.Green;
            this.ScreenBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScreenBox.ForeColor = System.Drawing.Color.White;
            this.ScreenBox.Location = new System.Drawing.Point(200, 143);
            this.ScreenBox.Name = "ScreenBox";
            this.ScreenBox.Size = new System.Drawing.Size(165, 117);
            this.ScreenBox.TabIndex = 2;
            this.ScreenBox.Text = "100%";
            this.ScreenBox.Visible = false;
            this.ScreenBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseDown);
            this.ScreenBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseMove);
            this.ScreenBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseUp);
            // 
            // timerSet
            // 
            this.timerSet.Interval = 14;
            this.timerSet.Tick += new System.EventHandler(this.timerSet_Tick);
            // 
            // picHeight
            // 
            this.picHeight.Location = new System.Drawing.Point(167, 143);
            this.picHeight.Name = "picHeight";
            this.picHeight.Size = new System.Drawing.Size(27, 117);
            this.picHeight.TabIndex = 3;
            this.picHeight.TabStop = false;
            this.picHeight.Visible = false;
            // 
            // picWidth
            // 
            this.picWidth.Location = new System.Drawing.Point(203, 113);
            this.picWidth.Name = "picWidth";
            this.picWidth.Size = new System.Drawing.Size(162, 27);
            this.picWidth.TabIndex = 3;
            this.picWidth.TabStop = false;
            this.picWidth.Visible = false;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.BackColor = System.Drawing.Color.Transparent;
            this.labelInstructions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions.ForeColor = System.Drawing.Color.White;
            this.labelInstructions.Location = new System.Drawing.Point(12, 9);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(131, 42);
            this.labelInstructions.TabIndex = 4;
            this.labelInstructions.Text = "Drag the mouse\r\nEscape to close";
            // 
            // labelExtraInfo
            // 
            this.labelExtraInfo.AutoSize = true;
            this.labelExtraInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelExtraInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExtraInfo.ForeColor = System.Drawing.Color.White;
            this.labelExtraInfo.Location = new System.Drawing.Point(12, 51);
            this.labelExtraInfo.Name = "labelExtraInfo";
            this.labelExtraInfo.Size = new System.Drawing.Size(72, 17);
            this.labelExtraInfo.TabIndex = 4;
            this.labelExtraInfo.Text = "P for Paint";
            // 
            // FormDrag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(593, 393);
            this.ControlBox = false;
            this.Controls.Add(this.picWidth);
            this.Controls.Add(this.picHeight);
            this.Controls.Add(this.ScreenBox);
            this.Controls.Add(this.labelExtraInfo);
            this.Controls.Add(this.labelInstructions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDrag";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Drag";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDrag_FormClosing);
            this.Load += new System.EventHandler(this.FormDrag_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormDrag_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ScreenBox;
        private System.Windows.Forms.Timer timerSet;
        private System.Windows.Forms.PictureBox picHeight;
        private System.Windows.Forms.PictureBox picWidth;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.Label labelExtraInfo;
    }
}
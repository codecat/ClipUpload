using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddonHelper {
    public partial class FormAnimationBox : Form {
        public int borderWidth = 4;
        public Brush borderBrush = Brushes.Red;

        public FormAnimationBox(Rectangle screenRect) {
            InitializeComponent();

            this.Left = screenRect.X - borderWidth;
            this.Top = screenRect.Y - borderWidth;
            this.Width = screenRect.Width + borderWidth * 2;
            this.Height = screenRect.Height + borderWidth * 2;

            Bitmap imgBorder = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(imgBorder);

            g.Clear(Color.Black);
            g.FillRectangle(borderBrush, new Rectangle(0, 0, this.borderWidth, this.Height)); // Left
            g.FillRectangle(borderBrush, new Rectangle(this.Width - this.borderWidth, 0, this.borderWidth, this.Height)); // Right
            g.FillRectangle(borderBrush, new Rectangle(0, 0, this.Width, this.borderWidth)); // Up
            g.FillRectangle(borderBrush, new Rectangle(0, this.Height - this.borderWidth, this.Width, this.borderWidth)); // Down

            this.BackgroundImage = imgBorder;
        }
    }
}

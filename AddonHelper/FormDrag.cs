using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace AddonHelper {
    public partial class FormDrag : Form {
        Settings settings;

        Rectangle[] Screens;

        public bool Dragging = false;
        int StartX = 0;
        int StartY = 0;
        int NowX = 0;
        int NowY = 0;

        Image beginImage;

        Rectangle Selection;

        public Action<Image> DoneDragging = null;

        public FormDrag() {
            InitializeComponent();

            this.settings = new Settings("settings.txt");
            if (this.settings.GetBool("DragExtra")) {
                this.labelExtraInfo.Visible = true;
                this.labelExtraInfo.Text = "P for " + this.settings.GetString("DragExtraName");
            }

            this.Screens = new Rectangle[Screen.AllScreens.Length];

            for (int i = 0; i < this.Screens.Length; i++)
                this.Screens[i] = Screen.AllScreens[i].Bounds;

            this.Width = this.Screens[0].Width;
            this.Height = this.Screens[0].Height;

            // This works because WinForms has an internal way of setting the maximum width/height of a Control (Form)
            // Check it with Reflector, System.Windows.Forms.Control.Width (set)
            for (int i = 0; i < Screen.AllScreens.Length; i++) {
                if (Screen.AllScreens[i].Bounds.Left < this.Left) {
                    this.Width += this.Left - Screen.AllScreens[i].Bounds.Left;
                    this.Left = Screen.AllScreens[i].Bounds.Left;
                } else
                    this.Width += Screen.AllScreens[i].Bounds.Width;

                if (Screen.AllScreens[i].Bounds.Top < this.Top) {
                    this.Height += this.Top - Screen.AllScreens[i].Bounds.Top;
                    this.Top = Screen.AllScreens[i].Bounds.Top;
                } else
                    this.Height += Screen.AllScreens[i].Bounds.Height;
            }

            this.Opacity = 0.5f;
            this.TopMost = true;

            this.beginImage = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics beginGfx = Graphics.FromImage(this.beginImage);
            beginGfx.CopyFromScreen(this.Location, Point.Empty, this.Size);
            beginGfx.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (keyData == Keys.Escape)
                this.Close();
            else if (keyData == Keys.P && this.labelExtraInfo.Visible) {
                if (this.Selection.Width > 0 && this.Selection.Height > 0) {
                    string exePath = this.settings.GetString("DragExtraPath");
                    string localTempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\" + Addon.RandomString(16) + ".png";

                    Bitmap img = null;
                    try {
                        img = this.SelectionImage;
                    } catch { }

                    if (img != null) {
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, ImageFormat.Png);
                        File.WriteAllBytes(localTempPath, ms.GetBuffer());

                        if (DoneDragging != null) {
                            //TODO: What do with this? Could return NULL as image, but then every addon must respond
                            //      accordingly to that. Can also not be triggered, but could break some addon code
                            //      in the progress.
                        }

                        if (File.Exists(exePath))
                            Process.Start(exePath, "\"" + localTempPath + "\"");
                        this.Close();
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        Rectangle GetInvertedRectangle(int PosX, int PosY, int SizeW, int SizeH) {
            Rectangle r = new Rectangle();

            r.X = PosX;
            r.Y = PosY;

            if (SizeW < 0) {
                r.X += SizeW;
                r.Width = -SizeW;
            } else
                r.Width = SizeW;

            if (SizeH < 0) {
                r.Y += SizeH;
                r.Height = -SizeH;
            } else
                r.Height = SizeH;

            return r;
        }

        void SetArea() {
            this.ScreenBox.Left = this.Selection.X;
            this.ScreenBox.Top = this.Selection.Y;
            this.ScreenBox.Width = this.Selection.Width;
            this.ScreenBox.Height = this.Selection.Height;

            {
                bool flipAround = this.ScreenBox.Top - this.picWidth.Height < 0;

                this.picWidth.Left = this.ScreenBox.Left;
                this.picWidth.Top = !flipAround ? (this.ScreenBox.Top - this.picWidth.Height) : (this.ScreenBox.Top + this.ScreenBox.Height);
                this.picWidth.Width = this.ScreenBox.Width > 0 ? this.ScreenBox.Width : 1;

                Bitmap bmp = new Bitmap(this.picWidth.Width, this.picWidth.Height);
                Graphics g = Graphics.FromImage(bmp);

                g.DrawLine(Pens.White, new Point(0, 0), new Point(0, bmp.Height));
                g.DrawLine(Pens.White, new Point(0, bmp.Height / 2), new Point(bmp.Width, bmp.Height / 2));
                g.DrawLine(Pens.White, new Point(bmp.Width - 1, 0), new Point(bmp.Width - 1, bmp.Height));

                g.DrawString(this.Selection.Width.ToString(), SystemFonts.DefaultFont, Brushes.White, new PointF(!flipAround ? 10 : bmp.Width - 40, 0));

                this.picWidth.Image = bmp;
            }

            {
                bool flipAround = this.ScreenBox.Left - this.picHeight.Width < 0;

                this.picHeight.Left = !flipAround ? (this.ScreenBox.Left - this.picHeight.Width) : (this.ScreenBox.Left + this.ScreenBox.Width);
                this.picHeight.Top = this.ScreenBox.Top;
                this.picHeight.Height = this.ScreenBox.Height > 0 ? this.ScreenBox.Height : 1;

                Bitmap bmp = new Bitmap(this.picHeight.Width, this.picHeight.Height);
                Graphics g = Graphics.FromImage(bmp);

                g.DrawLine(Pens.White, new Point(0, 0), new Point(bmp.Width, 0));
                g.DrawLine(Pens.White, new Point(bmp.Width / 2, 0), new Point(bmp.Width / 2, bmp.Height));
                g.DrawLine(Pens.White, new Point(0, bmp.Height - 1), new Point(bmp.Width, bmp.Height - 1));

                g.TranslateTransform(0, !flipAround ? 40 : bmp.Height - 10);
                g.RotateTransform(-90);
                g.DrawString(this.Selection.Height.ToString(), SystemFonts.DefaultFont, Brushes.White, new PointF());

                this.picHeight.Image = bmp;
            }
        }

        private void FormDrag_Load(object sender, EventArgs e) {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void FormDrag_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.labelInstructions.Text = "Release to upload\nRight mouse button to cancel";

                this.ScreenBox.Visible = true;
                this.picWidth.Visible = true;
                this.picHeight.Visible = true;

                int X = e.X;
                int Y = e.Y;
                if (sender.GetType() == typeof(Label)) {
                    X += this.ScreenBox.Left;
                    Y += this.ScreenBox.Top;
                }

                this.StartX = X;
                this.StartY = Y;

                this.Selection = new Rectangle(e.X, e.Y, 1, 1);
                this.SetArea();

                this.timerSet.Enabled = true;
            } else if (e.Button == MouseButtons.Right && this.ScreenBox.Visible) {
                this.labelInstructions.Text = "Drag the mouse\nEscape to close";

                this.Selection = new Rectangle();

                this.ScreenBox.Visible = false;
                this.picWidth.Visible = false;
                this.picHeight.Visible = false;

                this.timerSet.Enabled = false;
            }
        }

        Bitmap SelectionImage {
            get {
                Bitmap img = new Bitmap(this.Selection.Width, this.Selection.Height);
                Graphics.FromImage(img).DrawImage(this.beginImage, new Rectangle(0, 0, img.Width, img.Height), this.Selection, GraphicsUnit.Pixel);
                return img;
            }
        }

        private void FormDrag_MouseUp(object sender, MouseEventArgs e) {
            this.timerSet.Enabled = false;

            if (e.Button == MouseButtons.Left && this.ScreenBox.Visible) {
                if (this.Selection.Width > 0 && this.Selection.Height > 0) {
                    this.Hide();
                    Application.DoEvents();

                    if (DoneDragging != null)
                        DoneDragging.Invoke(this.SelectionImage);

                    this.Close();
                }
            }
        }

        private void FormDrag_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int X = e.X;
                int Y = e.Y;

                if (sender.GetType() == typeof(Label)) {
                    X += this.ScreenBox.Left;
                    Y += this.ScreenBox.Top;
                }

                this.NowX = X;
                this.NowY = Y;
            }
        }

        private void timerSet_Tick(object sender, EventArgs e) {
            this.Selection = GetInvertedRectangle(this.StartX, this.StartY, this.NowX - this.StartX, this.NowY - this.StartY);
            this.SetArea();
        }
    }
}

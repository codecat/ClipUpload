using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AddonHelper.Properties;
using System.IO;
using System.Drawing.Drawing2D;

namespace AddonHelper {
    public partial class FormEditor : Form {
        private Image img;
        private Graphics g;

        private Action<DragCallback> callback;

        private Point previewStart;
        private Rectangle previewRect;

        private Color currentColor = Color.Black;
        private Font currentFont = SystemFonts.DefaultFont;

        private List<Point> drawLines = new List<Point>();
        private Point prevLine = Point.Empty;

        public FormEditor(Image img, Action<DragCallback> callback) {
            InitializeComponent();

            this.img = img;
            this.g = Graphics.FromImage(this.img);

            this.callback = callback;

            this.Stage.Image = img;

            this.Width = img.Width + 50;
            this.Height = img.Height + 100;
        }

        private void FormEditor_Load(object sender, EventArgs e) {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            this.Close();
            this.callback(new DragCallback() { Type = DragCallbackType.Image, Image = this.img });
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void buttonCover_Click(object sender, EventArgs e) {
            this.Stage.Cursor = buttonCover.Checked ? Cursors.Cross : Cursors.Default;

            this.buttonText.Checked = false;
            this.buttonDraw.Checked = false;
            this.buttonCrop.Checked = false;
        }

        private void buttonText_Click(object sender, EventArgs e) {
            this.Stage.Cursor = buttonText.Checked ? Cursors.IBeam : Cursors.Default;

            this.buttonCover.Checked = false;
            this.buttonDraw.Checked = false;
            this.buttonCrop.Checked = false;
        }

        private void buttonDraw_Click(object sender, EventArgs e) {
            this.Stage.Cursor = Cursors.Default;

            this.buttonCover.Checked = false;
            this.buttonText.Checked = false;
            this.buttonCrop.Checked = false;
        }

        private void buttonCrop_Click(object sender, EventArgs e) {
            this.Stage.Cursor = buttonCrop.Checked ? Cursors.Cross : Cursors.Default;

            this.buttonCover.Checked = false;
            this.buttonText.Checked = false;
            this.buttonDraw.Checked = false;
        }

        private void timerSet_Tick(object sender, EventArgs e) {
            if (buttonCover.Checked || buttonCrop.Checked) {
                this.boxPreview.Location = new Point(this.previewRect.X + this.Stage.Left, this.previewRect.Y + this.Stage.Top);
                this.boxPreview.Size = this.previewRect.Size;

                if (this.previewRect.Width > 0 && this.previewRect.Height > 0) {
                    Bitmap rectBmp = new Bitmap(this.previewRect.Width, this.previewRect.Height);
                    Graphics rectGfx = Graphics.FromImage(rectBmp);

                    rectGfx.DrawImage(this.img, new Rectangle(0, 0, this.previewRect.Width, this.previewRect.Height), this.previewRect, GraphicsUnit.Pixel);
                    Pen rectPen = new Pen(this.currentColor, 5);
                    rectGfx.DrawRectangle(rectPen, new Rectangle(0, 0, this.previewRect.Width, this.previewRect.Height));

                    this.boxPreview.Image = rectBmp;
                }
            }

            if (buttonDraw.Checked) {
                if (this.drawLines.Count > 1) {
                    g.DrawLines(new Pen(this.currentColor, 4), this.drawLines.ToArray());
                    Point lastPoint = this.drawLines.Last();
                    this.drawLines.Clear();
                    this.drawLines.Add(lastPoint);
                }
            }

            this.Stage.Image = img;
        }

        private void Stage_MouseDown(object sender, MouseEventArgs e) {
            this.timerSet.Enabled = true;

            if (buttonCover.Checked || buttonCrop.Checked) {
                this.previewStart = new Point(e.X, e.Y);
                this.previewRect = new Rectangle(this.previewStart, new Size(1, 1));

                this.boxPreview.Location = this.previewStart;
                this.boxPreview.Size = Size.Empty;
                this.boxPreview.Visible = true;
            }

            if (buttonDraw.Checked) {
                this.drawLines.Clear();
                this.drawLines.Add(new Point(e.X, e.Y));
                this.prevLine = this.drawLines[0];
            }
        }

        private void Stage_MouseMove(object sender, MouseEventArgs e) {
            if (buttonCover.Checked || buttonCrop.Checked) {
                this.previewRect = GetInvertedRectangle(this.previewStart.X, this.previewStart.Y, e.X - this.previewStart.X, e.Y - this.previewStart.Y);
            }

            if (buttonDraw.Checked) {
                this.drawLines.Add(new Point(e.X, e.Y));
            }
        }

        private void Stage_MouseUp(object sender, MouseEventArgs e) {
            this.timerSet.Enabled = false;

            if (buttonCover.Checked) {
                this.boxPreview.Visible = false;
                g.FillRectangle(new SolidBrush(currentColor), previewRect);
            }

            if (buttonCrop.Checked) {
                this.boxPreview.Visible = false;

                if (this.previewRect.Width > 0 && this.previewRect.Height > 0) {
                    Bitmap cropBmp = new Bitmap(this.previewRect.Width, this.previewRect.Height);
                    Graphics cropGfx = Graphics.FromImage(cropBmp);
                    cropGfx.DrawImage(this.img, new Point(-this.previewRect.X, -this.previewRect.Y));
                    this.img = cropBmp;
                    this.g = cropGfx;
                }
            }

            if (buttonText.Checked) {
                this.previewStart = new Point(e.X, e.Y);

                toolStrip.Enabled = false;

                textPreview.Location = new Point(this.previewStart.X, this.previewStart.Y + this.Stage.Top);
                textPreview.Width = this.img.Width - textPreview.Left;
                textPreview.Font = this.currentFont;
                textPreview.Visible = true;
                textPreview.Focus();
            }

            if (buttonDraw.Checked) {
                this.drawLines.Add(new Point(e.X, e.Y));
                timerSet_Tick(null, null);
            }

            this.Stage.Image = img;
        }

        private void buttonAppend_Click(object sender, EventArgs e) {
            // TODO: Calling this.Hide is too slow, and there's no way to get a
            //       confirmation when a hiding animation has stopped. I need
            //       to find a better way to do this.
            //this.Hide();

            new FormDrag(null) {
                AnimationAllowed = false,
                DoneDragging = new Action<DragCallback>(delegate(DragCallback cb) {
                    if (cb.Type == DragCallbackType.Image) {
                        Image addImg = cb.Image;

                        int w = Math.Max(this.img.Width, addImg.Width);
                        int h = this.img.Height + addImg.Height;

                        Bitmap addBmp = new Bitmap(w, h);
                        Graphics addGfx = Graphics.FromImage(addBmp);

                        addGfx.DrawImage(this.img, Point.Empty);
                        addGfx.DrawImage(addImg, new Point(0, this.img.Height));

                        this.img = addBmp;
                        this.g = addGfx;

                        this.Stage.Image = this.img;
                    }
                })
            }.ShowDialog();

            // See TODO above
            //this.Show();
        }

        private void buttonPickColor_Click(object sender, EventArgs e) {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                currentColor = dialog.Color;
            }
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

        private void textPreview_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                g.DrawString(textPreview.Text, this.currentFont, new SolidBrush(this.currentColor), this.previewStart);
                this.Stage.Image = this.img;
            }

            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) {
                textPreview.Text = "";
                textPreview.Visible = false;

                toolStrip.Enabled = true;

                this.Stage.Focus();
            }
        }

        private void buttonFont_Click(object sender, EventArgs e) {
            FontDialog dialog = new FontDialog();
            dialog.Font = this.currentFont;
            if (dialog.ShowDialog() == DialogResult.OK) {
                this.currentFont = dialog.Font;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gif.Components;
using System.Runtime.InteropServices;

namespace AddonHelper {
    public partial class FormAnimation : Form {
        private Addon addon;
        private FormDrag dragForm;

        private Rectangle screenRectangle;
        private Action<DragCallback> doneDragging;

        private List<Image> frames = new List<Image>();

        private FormAnimationBox animBox;

        private bool initialized = false;
        private int spawnLocation = 0;

        Point screenOffset() {
            Point ret = new Point();
            foreach (Screen screen in Screen.AllScreens) {
                if (ret.X > screen.Bounds.X)
                    ret.X = screen.Bounds.X;
                if (ret.Y > screen.Bounds.Y)
                    ret.Y = screen.Bounds.Y;
            }
            return ret;
        }

        public FormAnimation(FormDrag dragForm, Rectangle rect, Action<DragCallback> callback) {
            InitializeComponent();

            this.addon = dragForm.addon;
            this.dragForm = dragForm;

            this.timerFrame.Interval = 1000 / dragForm.settings.GetInt("DragAnimFPS");

            rect.Width -= rect.Width % 4;
            rect.Height -= rect.Height % 4;

            this.screenRectangle = rect;
            this.doneDragging = callback;

            this.Left = this.screenRectangle.X;
            this.Top = this.screenRectangle.Y + this.screenRectangle.Height + 14;

            if(this.Top + this.Height > Screen.PrimaryScreen.WorkingArea.Height) {
                this.Top = this.screenRectangle.Y - this.Height - 14;
                spawnLocation = 1;
            }

            Point offset = screenOffset();
            this.Left += offset.X;
            this.Top += offset.Y;

            this.animBox = new FormAnimationBox(rect);
            this.animBox.Left += offset.X;
            this.animBox.Top += offset.Y;
            this.animBox.Show();

            this.trackScale.Value = dragForm.settings.GetInt("DragAnimScale");

            if(dragForm.settings.GetBool("DragAnimAuto")) {
                buttonRecord_Click(null, null);
            }

            initialized = true;
        }

        private void buttonRecord_Click(object sender, EventArgs e) {
            buttonRecord.Enabled = false;
            buttonStop.Enabled = true;
            buttonRedo.Enabled = true;
            trackScale.Enabled = false;

            timerFrame.Enabled = true;
        }

        private void buttonRedo_Click(object sender, EventArgs e) {
            buttonRecord.Enabled = true;
            buttonStop.Enabled = false;
            buttonRedo.Enabled = false;
            trackScale.Enabled = true;

            timerFrame.Enabled = false;

            this.frames.Clear();

            this.Status.Text = "0.0 seconds, 0 frames";
        }

        private void buttonStop_Click(object sender, EventArgs e) {
            this.timerFrame.Enabled = false;

            this.Hide();
            this.animBox.Hide();

            DragCallback callback = new DragCallback() { Type = DragCallbackType.Animation, Animation = new MemoryStream() };

            AnimatedGifEncoder encoder = new AnimatedGifEncoder();
            encoder.Start(callback.Animation);
            encoder.SetDelay(this.timerFrame.Interval);
            encoder.SetRepeat(0);

            this.addon.ProgressBar.Start("Encoding Gif", this.frames.Count, false);

            for(int i = 0; i < this.frames.Count; i++) {
                if(this.addon.ProgressBar.Canceled) {
                    this.Close();
                    return;
                }

                encoder.AddFrame(this.frames[i]);
                this.addon.ProgressBar.Set(i);
            }

            encoder.Finish();

            this.addon.ProgressBar.Done();

            this.doneDragging(callback);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void timerFrame_Tick(object sender, EventArgs e) {
            double scale = trackScale.Value / 100d;

            int w = (int)(screenRectangle.Width * scale); w -= w % 4;
            int h = (int)(screenRectangle.Height * scale); h -= h % 4;

            if(w == 0 || h == 0) {
                buttonRedo_Click(null, null);
                return;
            }

            Bitmap bmp = new Bitmap(screenRectangle.Width, screenRectangle.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(screenRectangle.Location, Point.Empty, bmp.Size);

            Rectangle cursorRect = new Rectangle(Cursor.Position.X - screenRectangle.X, Cursor.Position.Y - screenRectangle.Y, 32, 32);
            Cursor.Draw(g, cursorRect);

            g.Dispose();

            Bitmap bmp2 = new Bitmap(w, h);
            g = Graphics.FromImage(bmp2);
            g.DrawImage(bmp, new Rectangle(0, 0, w, h));
            g.Dispose();

            this.frames.Add(bmp2);

            int frameCount = this.frames.Count;
            double seconds = this.timerFrame.Interval * frameCount / 1000d;
            this.Status.Text = seconds.ToString("F1") + " seconds, " + frameCount + " frames";
        }

        private void FormAnimation_FormClosing(object sender, FormClosingEventArgs e) {
            this.animBox.Close();
            this.dragForm.settings.SetInt("DragAnimScale", trackScale.Value);
            this.dragForm.settings.Save();
        }

        private void FormAnimation_LocationChanged(object sender, EventArgs e) {
            if(!initialized) return;

            this.screenRectangle.X = this.Left;
            if(spawnLocation == 0) {
                this.screenRectangle.Y = this.Top - this.screenRectangle.Height - 14;
            } else if(spawnLocation == 1) {
                this.screenRectangle.Y = this.Top + this.Height + 14;
            }

            this.animBox.Left = this.screenRectangle.X - this.animBox.borderWidth;
            this.animBox.Top = this.screenRectangle.Y - this.animBox.borderWidth;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace ClipUpload3 {
    public partial class FormUploadLog : Form {
        public FormUploadLog() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to clear the upload log completely? This can not be undone!", "Upload Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                File.Delete("uploadlog.txt");
                listLog.Items.Clear();
            }
        }

        void LoadList() {
            listLog.Items.Clear();
            if (File.Exists("uploadlog.txt")) {
                FileStream fs = File.OpenRead("uploadlog.txt");
                StreamReader reader = new StreamReader(fs);

                string Line = "";
                while (!reader.EndOfStream) {
                    Line = reader.ReadLine().Trim();
                    if (Line != "") {
                        string[] parse = Line.Split(new char[] { '|' }, 2);
                        if (parse.Length == 2) {
                            ListViewItem lvi = listLog.Items.Add(parse[0]);
                            lvi.SubItems.Add(parse[1]);
                        }
                    }
                }

                fs.Close();
            }
        }

        private void FormUploadLog_Load(object sender, EventArgs e) {
            LoadList();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetOpenClipboardWindow();

        public void SetClipboardText(string Text) {
            int timeout = 5;
            while (timeout-- > 0) {
                IntPtr cbTry = GetOpenClipboardWindow();
                if (cbTry == IntPtr.Zero) break;
                System.Threading.Thread.Sleep(100);
            }
            try {
                Clipboard.SetText(Text);
            } catch { }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (listLog.SelectedItems.Count == 1) {
                ListViewItem selectedItem = listLog.SelectedItems[0];
                SetClipboardText(selectedItem.SubItems[1].Text);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (listLog.SelectedItems.Count == 1) {
                ListViewItem selectedItem = listLog.SelectedItems[0];
                SetClipboardText(selectedItem.Text);
            }
        }

        private void button5_Click(object sender, EventArgs e) {
            if (listLog.SelectedItems.Count == 1) {
                ListViewItem selectedItem = listLog.SelectedItems[0];
                SetClipboardText(selectedItem.Text + " | " + selectedItem.SubItems[1].Text);
            }
        }

        private void button6_Click(object sender, EventArgs e) {
            if (listLog.SelectedItems.Count == 1) {
                ListViewItem selectedItem = listLog.SelectedItems[0];
                Process.Start(selectedItem.SubItems[1].Text);
            }
        }

        int OriginalWidth = 0;
        bool Expanded = false;
        private void buttonExpand_Click(object sender, EventArgs e) {
            if (!Expanded) {
                OriginalWidth = this.Width;
                this.Width += 180;
                PreviewImage();

                buttonExpand.Text = "<<";
                Expanded = true;
            } else {
                this.Width = OriginalWidth;

                buttonExpand.Text = ">>";
                Expanded = false;
            }

            picturePreview.Visible = Expanded;
        }

        int DownloadImage = -1;
        public void PreviewImage() {
            if (!Expanded)
                return;

            if (listLog.SelectedItems.Count == 1) {
                string URL = listLog.SelectedItems[0].SubItems[1].Text;

                if (!URL.EndsWith(".png") && !URL.EndsWith(".gif") && !URL.EndsWith(".jpg"))
                    return;

                WebClient wc = new WebClient();
                wc.Proxy = null;
                wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);

                DownloadImage = listLog.SelectedItems[0].Index;
                picturePreview.Image = null;

                try { wc.DownloadDataAsync(new Uri(URL)); } catch { return; }
            }
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e) {
            if (listLog.SelectedItems.Count == 1 && listLog.SelectedItems[0].Index == DownloadImage) {
                try { picturePreview.Image = new Bitmap(new MemoryStream(e.Result)); } catch { }
            }
        }

        private void listLog_SelectedIndexChanged(object sender, EventArgs e) {
            PreviewImage();
        }
    }
}

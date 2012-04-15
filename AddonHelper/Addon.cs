using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace AddonHelper {
    public class Addon {
        public static Random rnd = new Random();

        public void AddLog(string URL) {
            StreamWriter writer;
            if (File.Exists("uploadlog.txt"))
                writer = File.AppendText("uploadlog.txt");
            else
                writer = new StreamWriter(File.Create("uploadlog.txt"));

            writer.WriteLine(DateTime.Now.ToString("G") + "|" + URL);
            writer.Close();
            writer.Dispose();
        }

        public void Drag(Action<Image> doneDragging) {
            FormDrag formDrag = new FormDrag();
            formDrag.DoneDragging = doneDragging;
            formDrag.ShowDialog();
        }

        public void PopulateKeysCombobox(ComboBox comboBox) {
            string[] keys = Enum.GetNames(typeof(Keys));
            foreach (string key in keys)
                comboBox.Items.Add(key);
        }

        public string MD5(string S) {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bOut = x.ComputeHash(Encoding.UTF8.GetBytes(S));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bOut)
                sb.Append(b.ToString("x2").ToLower());
            return sb.ToString();
        }

        public static string RandomString(int len, string chars = "abcdefghijklmnopqrstuvwxyz0123456789") {
            string ret = "";
            for (int i = 0; i < len; i++) {
                string a = chars[rnd.Next(chars.Length)].ToString();
                if (rnd.Next(2) == 0)
                    ret += a.ToUpper();
                else
                    ret += a;
            }
            return ret;
        }

        public string RandomFilename(int len, string chars = "abcdefghijklmnopqrstuvwxyz0123456789") {
            return RandomString(len, chars);
        }

        public string getDateString() {
            return DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }

        public string base64Encode(string input) {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(input));
        }

        public string base64Decode(string input) {
            return Encoding.ASCII.GetString(Convert.FromBase64String(input));
        }

        public string LongDataEscape(string Str) {
            string Output = "";
            int ByteCount = 32766;
            if (Str.Length > ByteCount) {
                for (int i = 0; i < Str.Length; i += ByteCount) {
                    if (Str.Length - i < ByteCount)
                        Output += Uri.EscapeDataString(Str.Substring(i, Str.Length - i));
                    else
                        Output += Uri.EscapeDataString(Str.Substring(i, ByteCount));
                }
            } else
                Output = Uri.EscapeDataString(Str);
            return Output;
        }

        public string GetBetween(string Source, string Str1, string Str2) {
            return Source.Split(new string[] { Str1, Str2 }, StringSplitOptions.None)[Source.Contains(Str1) ? 1 : 0];
        }

        public WebProxy GetProxy() {
            Settings settings = new Settings("settings.txt");

            if (settings.GetBool("ProxyEnabled")) {
                string hostName = settings.GetString("ProxyHost");
                int hostPort = settings.GetInt("ProxyPort");

                WebProxy ret = new WebProxy(hostName, hostPort);

                string credentialsUsername = settings.GetString("ProxyUsername");
                string credentialsPassword = settings.GetString("ProxyPassword");

                if (credentialsUsername != "" || credentialsPassword != "") {
                    ret.Credentials = new NetworkCredential(credentialsUsername, credentialsPassword);
                } else {
                    ret.UseDefaultCredentials = true;
                }

                return ret;
            } else
                return null;
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

        public class cProgressBar {
            public FormProgressBar Form;

            private string filename = "";
            private int filesize = 0;
            private int lastLocation = 0;
            private Stopwatch speedTimer = new Stopwatch();

            private bool done = false;
            public bool Canceled = false;

            public void Start(string Filename, long Filesize) {
                this.reset();

                Settings settings = new Settings("settings.txt");
                if (!settings.GetBool("ProgressBar")) {
                    return;
                }

                this.Form = new FormProgressBar(settings);
                this.Form.FormClosing += new FormClosingEventHandler(cancelUpload);
                this.Form.Show();

                this.filename = Filename;
                this.filesize = (int)Filesize; //TODO: Handle Filesize > int.MaxValue
                this.updateStatus(0);
            }

            private void reset() {
                if (this.Form != null && !this.Form.IsDisposed) {
                    this.Form.Close();
                }
                this.Form = null;

                this.filename = "";
                this.filesize = 0;
                this.lastLocation = 0;
                this.speedTimer = new Stopwatch();
                this.speedTimer.Start();

                this.done = false;
                this.Canceled = false;
            }

            private void cancelUpload(object sender, FormClosingEventArgs e) {
                if (!this.done) {
                    this.Canceled = true;
                }
            }

            private void doEvents() {
                Application.DoEvents();
            }

            private double percentage(int currentLocation) {
                return 100d / (double)this.filesize * (double)currentLocation;
            }

            private double percentage(int currentLocation, int decimals) {
                return Math.Round(this.percentage(currentLocation), decimals);
            }

            private void updateStatus(int currentLocation) {
                int bytesTransfered = currentLocation - this.lastLocation;
                double bytesPerSecond = Math.Round(this.percentage(currentLocation) / 100d * (double)this.filesize) / ((double)this.speedTimer.ElapsedMilliseconds / 1000d);
                double uploadRateBps = Math.Round(bytesPerSecond, 2);
                double uploadRateKBps = Math.Round(uploadRateBps / 1024, 2);
                double uploadRateMBps = Math.Round(uploadRateKBps / 1024, 2);
                this.lastLocation = currentLocation;

                string uploadRate = uploadRateBps + " B/s";
                if (uploadRateBps > 1024) {
                    uploadRate = uploadRateKBps + " KB/s";
                }
                if (uploadRateKBps > 1024) {
                    uploadRate = uploadRateMBps + " MB/s";
                }
                this.Form.Text = this.filename + " - " + this.percentage(currentLocation, 0) + "% - " + uploadRate;

                this.Form.progressBar.Value = 100; // First set it to 100 to bypass the Aero animation
                this.Form.progressBar.Value = (int)this.percentage(currentLocation);
            }

            public void Set(int currentLocation) {
                if (!Canceled && this.Form != null) {
                    this.updateStatus(currentLocation);
                    this.doEvents();
                }
            }

            public void Done() {
                if (!Canceled && this.Form != null) {
                    this.done = true;
                    this.Form.Close();
                }
            }
        }

        public cProgressBar ProgressBar = new cProgressBar();
    }
}

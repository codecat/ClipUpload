using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;
using System.Reflection;

namespace AddonHelper {
    public enum DragCallbackType { Image, Animation, None }
    public class DragCallback {
        public DragCallbackType Type;

        public Image Image;
        public MemoryStream Animation;
    }

    public class Addon {
        public static Random rnd = new Random();
        private Settings appSettings = new Settings("settings.txt");

        public void Debug(string str) {
            StreamWriter writer;
            if (File.Exists("debug.txt"))
                writer = File.AppendText("debug.txt");
            else
                writer = new StreamWriter(File.Create("debug.txt"));

            writer.WriteLine("[" + this.GetType().Name + " - " + DateTime.Now.ToString() + "] " + str);
            writer.Close();
            writer.Dispose();
        }

        public long Epoch() {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// Transforms an image based on global upload settings
        /// </summary>
        /// <param name="img">The base image</param>
        public void ImagePipeline(Image img) {
            Graphics gfx = Graphics.FromImage(img);

            // 2nd anniversary easter egg, cats!
            appSettings.Reload();
            if (appSettings.Contains("Cats")) {
                try {
                    // First, get a random cat picture
                    // Can be fetched from placekitten.com/g/<width>/<height>
                    WebClient wc = new WebClient();
                    Image catImage = Image.FromStream(new MemoryStream(wc.DownloadData("http://" + "placekitten.com/g/" + img.Width + "/" + img.Height)));

                    // Create attributes that transform the image before pasting
                    ImageAttributes imgAttributes = new ImageAttributes();
                    imgAttributes.SetColorMatrix(new ColorMatrix() { Matrix33 = 0.5f }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); // Matrix33 = alpha

                    // Now we draw the cat picture with the attributes
                    gfx.DrawImage(catImage, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
                } catch { }
            }

            gfx.Dispose();
        }

        public static void SetShortcuts(bool enabled) {
            Form form = null;
            foreach(Form f in Application.OpenForms) {
                if(f.GetType().Name == "FormMain") {
                    form = f;
                    break;
                }
            }

            FieldInfo fiListener = form.GetType().GetField("keyboardListener");
            PropertyInfo piEnabled = fiListener.FieldType.GetProperty("Enabled");
            piEnabled.SetValue(fiListener.GetValue(form), enabled, null);
        }

        private string formatFilename(string origFilename) {
            string fnm = appSettings.GetString("BackupsFormat");
            fnm = fnm.Replace("%ADDON%", this.GetType().Name);
            fnm = fnm.Replace("%DATE%", DateTime.Now.ToString("d").Replace('/', '-'));
            fnm = fnm.Replace("%TIME%", DateTime.Now.ToString("t").Replace(':', '.'));
            fnm = fnm.Replace("%EPOCH%", Epoch().ToString());
            fnm = fnm.Replace("%EPOCHX%", Epoch().ToString("x8"));
            fnm = fnm.Replace("%FILENAME%", origFilename);

            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
                fnm = fnm.Replace(c.ToString(), "");

            return fnm;
        }

        public void Backup(string sourceFile) {
            if (appSettings.GetBool("BackupsEnabled")) {
                string path = appSettings.GetString("BackupsPath");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string filename = Path.GetFileName(sourceFile);
                string fnm = formatFilename(filename);
                File.Copy(sourceFile, path + "/" + fnm);
            }
        }

        public void Backup(byte[] buffer, string filename) {
            if (appSettings.GetBool("BackupsEnabled")) {
                string path = appSettings.GetString("BackupsPath");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fnm = formatFilename(filename);
                File.WriteAllBytes(path + "/" + fnm, buffer);
            }
        }

        public void AddLog(string URL, string info) {
            Form form = null;
            foreach (Form f in Application.OpenForms) {
                if (f.GetType().Name == "FormMain") {
                    form = f;
                    break;
                }
            }

            MethodInfo mi = form.GetType().GetMethod("JustUploaded");
            mi.Invoke(form, new string[] { URL, info });

            StreamWriter writer;
            if (File.Exists("uploadlog.txt"))
                writer = File.AppendText("uploadlog.txt");
            else
                writer = new StreamWriter(File.Create("uploadlog.txt"));

            writer.WriteLine(DateTime.Now.ToString("G") + "|" + URL);
            writer.Close();
            writer.Dispose();
        }

        public void Drag(Action<DragCallback> doneDragging) {
            FormDrag formDrag = new FormDrag(this);
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
            if (appSettings.GetBool("ProxyEnabled")) {
                string hostName = appSettings.GetString("ProxyHost");
                int hostPort = appSettings.GetInt("ProxyPort");

                WebProxy ret = new WebProxy(hostName, hostPort);

                string credentialsUsername = appSettings.GetString("ProxyUsername");
                string credentialsPassword = appSettings.GetString("ProxyPassword");

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
            } catch (Exception ex) {
                this.Debug("Clipboard.SetText threw " + ex.GetType().FullName + ": '" + ex.Message + "'");
            }
        }

        public ImageCodecInfo GetEncoder(ImageFormat format) {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        public class cProgressBar {
            public FormProgressBar Form;

            private string filename = "";
            private int filesize = 0;
            private int lastLocation = 0;
            private bool showSpeed = true;
            private Stopwatch speedTimer = new Stopwatch();

            private bool done = false;
            public bool Canceled = false;

            private Settings appSettings = new Settings("settings.txt");

            public void Start(string Filename, long Filesize) {
                this.Start(Filename, Filesize, true);
            }

            public void Start(string Filename, long Filesize, bool DisplaySpeed) {
                Addon.SetShortcuts(false);

                this.reset();
                if (Filesize == 0) return;

                if (!appSettings.GetBool("ProgressBar")) {
                    return;
                }

                this.Form = new FormProgressBar(appSettings);
                this.Form.FormClosing += new FormClosingEventHandler(cancelUpload);

                new Thread(new ThreadStart(delegate {
                    this.filename = Filename;
                    this.filesize = (int)Filesize; //TODO: Handle Filesize > int.MaxValue
                    this.showSpeed = DisplaySpeed;
                    this.updateStatus(0);

                    Application.Run(this.Form);
                })).Start();
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

            private double percentage(int currentLocation) {
                return Math.Min(100d, 100d / (double)this.filesize * (double)currentLocation);
            }

            private double percentage(int currentLocation, int decimals) {
                return Math.Round(this.percentage(currentLocation), decimals);
            }

            private void updateStatus(int currentLocation) {
                this.lastLocation = currentLocation;

                string uploadRate = "";
                if (this.showSpeed) {
                    int bytesTransfered = currentLocation - this.lastLocation;
                    double bytesPerSecond = Math.Round(this.percentage(currentLocation) / 100d * (double)this.filesize) / ((double)this.speedTimer.ElapsedMilliseconds / 1000d);
                    double uploadRateBps = Math.Round(bytesPerSecond, 2);
                    double uploadRateKBps = Math.Round(uploadRateBps / 1024, 2);
                    double uploadRateMBps = Math.Round(uploadRateKBps / 1024, 2);

                    uploadRate = uploadRateBps + " B/s";
                    if (uploadRateBps > 1024)
                        uploadRate = uploadRateKBps + " KB/s";
                    if (uploadRateKBps > 1024)
                        uploadRate = uploadRateMBps + " MB/s";
                }


                this.Form.Text = this.filename + " - " + this.percentage(currentLocation, 0) + "%" + (this.showSpeed ? " - " + uploadRate : "");

                int percVal = (int)this.percentage(currentLocation);
                this.Form.progressBar.Value = percVal + 1; // First set it to it's value + 1 to bypass the Aero animation
                this.Form.progressBar.Value = percVal;
            }

            public void Set(int currentLocation) {
                if (!Canceled && this.Form != null) {
                    try {
                        this.Form.Invoke(new Action(delegate {
                            this.updateStatus(currentLocation);
                        }));
                    } catch { }
                }
            }

            public void Done() {
                Addon.SetShortcuts(true);

                if (!Canceled && this.Form != null) {
                    this.done = true;
                    try {
                        this.Form.Invoke(new Action(delegate {
                            this.Form.Close();
                        }));
                    } catch { }
                }
            }
        }

        public cProgressBar ProgressBar = new cProgressBar();
    }
}

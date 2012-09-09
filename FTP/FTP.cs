using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Specialized;
using AddonHelper;

namespace FTP {
    public class FTP : Addon {
        public Settings settings;

        public NotifyIcon Tray;

        public string ftpServer = "";
        public string ftpUsername = "";
        public string ftpPassword = "";
        public string ftpPath = "";
        public bool ftpPassive = false;
        public bool ftpBinary = true;
        public string ftpHttp = "";
        public string imageFormat = "PNG";
        public bool useMD5 = false;
        public bool shortMD5 = false;
        public int length = 8;

        public bool jpegCompression = false;
        public int jpegCompressionFilesize = 1000;
        public int jpegCompressionRate = 75;

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Bitmap bmpIcon;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/FTP/settings.txt");
            this.bmpIcon = new Icon("Addons/FTP/Icon.ico").ToBitmap();

            LoadSettings();
        }

        public void LoadSettings() {
            if (settings.Contains("ShortcutModifiers")) {
                // Migrate old 3.00 config file
                settings.SetBool("Binary", true);

                settings.SetString("ShortcutDragModifiers", settings.GetString("ShortcutModifiers"));
                settings.SetString("ShortcutDragKey", settings.GetString("ShortcutKey"));
                settings.SetString("ShortcutPasteModifiers", "");
                settings.SetString("ShortcutPasteKey", "");

                settings.Delete("ShortcutModifiers");
                settings.Delete("ShortcutKey");

                settings.Save();
            }

            if (!settings.Contains("JpegCompression")) {
                // Migrate old 3.10 config file
                settings.SetBool("JpegCompression", false);
                settings.SetInt("JpegCompressionFilesize", 1000);
                settings.SetInt("JpegCompressionRate", 75);

                settings.Save();
            }

            ftpServer = settings.GetString("Server");
            ftpUsername = settings.GetString("Username");
            ftpPassword = base64Decode(settings.GetString("Password"));
            ftpPath = settings.GetString("Path");
            ftpPassive = settings.GetBool("Passive");
            ftpBinary = settings.GetBool("Binary");
            ftpHttp = settings.GetString("Http");

            imageFormat = settings.GetString("Format");

            useMD5 = settings.GetBool("UseMD5");
            shortMD5 = settings.GetBool("ShortMD5");

            length = settings.GetInt("Length");

            jpegCompression = settings.GetBool("JpegCompression");
            jpegCompressionFilesize = settings.GetInt("JpegCompressionFilesize");
            jpegCompressionRate = settings.GetInt("JpegCompressionRate");

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            if (ftpServer != "" && ftpUsername != "" && ftpPath != "" && ftpHttp != "") {
                Hashtable DragItem = new Hashtable();
                DragItem.Add("Visible", true);
                DragItem.Add("Text", "Drag -> FTP");
                DragItem.Add("Image", this.bmpIcon);
                DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
                DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
                DragItem.Add("ShortcutKey", this.shortCutDragKey);
                ret.Add(DragItem);

                Hashtable UpItem = new Hashtable();
                UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList() || Clipboard.ContainsText());
                UpItem.Add("Text", "FTP");
                UpItem.Add("Image", this.bmpIcon);
                UpItem.Add("Action", new Action(Upload));
                UpItem.Add("ShortcutModifiers", this.shortCutPasteModifiers);
                UpItem.Add("ShortcutKey", this.shortCutPasteKey);
                ret.Add(UpItem);
            }

            return ret.ToArray();
        }

        public void Settings() {
            new FormSettings(this).ShowDialog();
        }

        public void DragCallback(DragCallback callback) {
            switch (callback.Type) {
                case DragCallbackType.Image:
                    UploadImage(callback.Image);
                    break;

                case DragCallbackType.Animation:
                    UploadAnimation(callback.Animation);
                    break;
            }
        }

        public void UploadImage(Image img) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/FTP/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            string formatStr = imageFormat.ToLower();

            switch (formatStr) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            this.ImagePipeline(img);

            img.Save(ms, format);

            if (jpegCompression) {
                if (ms.Length / 1000 > jpegCompressionFilesize) {
                    ms.Dispose();
                    ms = new MemoryStream();

                    // Set up the encoder, codec and params
                    System.Drawing.Imaging.Encoder jpegEncoder = System.Drawing.Imaging.Encoder.Compression;
                    ImageCodecInfo jpegCodec = this.GetEncoder(ImageFormat.Jpeg);
                    EncoderParameters jpegParams = new EncoderParameters();
                    jpegParams.Param[0] = new EncoderParameter(jpegEncoder, jpegCompressionRate);

                    // Now save it with the new encoder
                    img.Save(ms, jpegCodec, jpegParams);

                    // And make sure the filename gets set correctly
                    formatStr = "jpg";
                }
            }

            bool result = false;
            string failReason = "";
            string filename = "";

            bool canceled = false;
            try {
                filename = this.RandomFilename(this.settings.GetInt("Length"));
                if (this.useMD5) {
                    filename = MD5(filename + rnd.Next(1000, 9999).ToString());

                    if (this.shortMD5)
                        filename = filename.Substring(0, this.length);
                }

                filename += "." + formatStr;

                this.Backup(ms.GetBuffer(), filename);
                canceled = !UploadToFTP(ms, filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = ftpHttp + filename;
                    this.AddLog(url, img.Width + " x " + img.Height);
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to FTP and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on your FTP server. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            img.Dispose();

            Tray.Icon = defIcon;
        }

        public void UploadAnimation(MemoryStream ms) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/FTP/Icon.ico");

            bool result = false;
            string failReason = "";
            string filename = "";

            bool canceled = false;
            try {
                filename = this.RandomFilename(this.settings.GetInt("Length"));
                if (this.useMD5) {
                    filename = MD5(filename + rnd.Next(1000, 9999).ToString());

                    if (this.shortMD5)
                        filename = filename.Substring(0, this.length);
                }

                filename += ".gif";

                canceled = !UploadToFTP(ms, filename);

                this.Backup(ms.GetBuffer(), filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = ftpHttp + filename;
                    this.AddLog(url, (ms.Length / 1000) + " kB");
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Animation uploaded to FTP and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on your FTP server. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadText(string Text) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/FTP/Icon.ico");

            bool result = false;
            string failReason = "";
            string filename = "";

            byte[] textData = Encoding.UTF8.GetBytes(Text);

            bool canceled = false;
            try {
                filename = this.RandomFilename(this.settings.GetInt("Length"));
                if (this.useMD5) {
                    filename = MD5(filename + rnd.Next(1000, 9999).ToString());

                    if (this.shortMD5)
                        filename = filename.Substring(0, this.length);
                }

                filename += ".txt";

                canceled = !UploadToFTP(new MemoryStream(textData), filename);

                this.Backup(textData, filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = ftpHttp + filename;
                    this.AddLog(url, Text.Length + " characters");
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Text uploaded to FTP and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on your FTP server. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadFiles(StringCollection files) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/FTP/Icon.ico");

            bool result = false;
            string failReason = "";
            string finalCopy = "";

            bool canceled = false;

            try {
                foreach (string file in files) {
                    string filename = file.Split('/', '\\').Last();

                    canceled = !UploadToFTP(new MemoryStream(File.ReadAllBytes(file)), filename);
                    if (canceled)
                        break;

                    string url = ftpHttp + Uri.EscapeDataString(filename);
                    this.AddLog(url, (new FileInfo(file).Length / 1000) + " kB");

                    finalCopy += url + "\n";
                }

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    this.SetClipboardText(finalCopy.Substring(0, finalCopy.Length - 1));
                    Tray.ShowBalloonTip(1000, "Upload success!", "File(s) uploaded to your FTP folder and URL(s) copied.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong, probably on your FTP server. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public bool UploadToFTP(MemoryStream ms, string filename) {
            this.ProgressBar.Start(filename, ms.Length);

            string strPath = ftpPath;
            if (!strPath.EndsWith("/")) {
                strPath += "/";
            }

            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create("ftp://" + ftpServer + strPath + filename);
            ftp.Proxy = null; //TODO: Ftp Proxy? (From SO: "If the specified proxy is an HTTP proxy, only the DownloadFile, ListDirectory, and ListDirectoryDetails commands are supported.")
            ftp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UsePassive = ftpPassive;
            ftp.UseBinary = ftpBinary;

            Stream stream = ftp.GetRequestStream();

            int sr = 1024;
            for (int i = 0; i < ms.Length; i += 1024) {
                if (ms.Length - i < 1024)
                    sr = (int)ms.Length - i;
                else
                    sr = 1024;

                byte[] buffer = new byte[sr];
                ms.Seek((long)i, SeekOrigin.Begin);
                ms.Read(buffer, 0, sr);
                stream.Write(buffer, 0, sr);

                if (this.ProgressBar.Canceled) {
                    // Remove the file from the server..
                    //TODO: Make this a setting?
                    FtpWebRequest ftpDelete = (FtpWebRequest)FtpWebRequest.Create("ftp://" + ftpServer + strPath + filename);
                    ftpDelete.Proxy = null;
                    ftpDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                    ftpDelete.Credentials = ftp.Credentials;
                    ftpDelete.UsePassive = ftpPassive;
                    ftpDelete.UseBinary = ftpBinary;
                    ftpDelete.GetResponse();
                    ftpDelete.Abort();

                    ftp.Abort();
                    ms.Dispose();

                    ftp = null;
                    ms = null;

                    return false;
                }
                this.ProgressBar.Set(i);
            }

            stream.Close();
            stream.Dispose();
            ftp.Abort();

            this.ProgressBar.Done();

            return true;
        }

        public void Upload() {
            if (Clipboard.ContainsImage())
                UploadImage(Clipboard.GetImage());
            else if (Clipboard.ContainsText())
                UploadText(Clipboard.GetText());
            else if (Clipboard.ContainsFileDropList()) {
                StringCollection files = Clipboard.GetFileDropList();
                if (files.Count == 1 && (files[0].EndsWith(".png") || files[0].EndsWith(".jpg") || files[0].EndsWith(".gif")))
                    UploadImage(Image.FromFile(files[0]));
                else
                    UploadFiles(files);
            }
        }
    }
}

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
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;


namespace S3 {
    public class S3 : Addon {
        public Settings settings;

        public NotifyIcon Tray;


        public string AWSKey = "";
        public string AWSSecretKey = "";
        public string bucketName = "";
        public string prefix = "";

        public bool isPrivate = true;
        public bool shorten = false;
        public bool https = true;
        public bool appendExt = false;

        public int expires = 7;

        public string imageFormat = "PNG";

        public string shortCutDragModifiers = "";
        public string shortCutDragKey = "";
        public string shortCutPasteModifiers = "";
        public string shortCutPasteKey = "";

        private Bitmap bmpIcon;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
            this.settings = new Settings("Addons/S3/settings.txt");
            this.bmpIcon = new Icon("Addons/S3/Icon.ico").ToBitmap();

            LoadSettings();
        }

        public void LoadSettings() {


            AWSKey = settings.GetString("AWSKey");
            bucketName = settings.GetString("bucketName");
            AWSSecretKey = base64Decode(settings.GetString("AWSSecretKey"));
            prefix = settings.GetString("prefix");
            isPrivate = settings.GetBool("private");
            expires = settings.GetInt("expires");
            https = settings.GetBool("https");
            shorten = settings.GetBool("shorten");
            appendExt = settings.GetBool("appendExt");

            imageFormat = settings.GetString("Format");

 

            shortCutDragModifiers = settings.GetString("ShortcutDragModifiers");
            shortCutDragKey = settings.GetString("ShortcutDragKey");
            shortCutPasteModifiers = settings.GetString("ShortcutPasteModifiers");
            shortCutPasteKey = settings.GetString("ShortcutPasteKey");
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            if (AWSKey != "" && AWSSecretKey != "" && bucketName != "") {
                Hashtable DragItem = new Hashtable();
                DragItem.Add("Visible", true);
                DragItem.Add("Text", "Drag -> S3");
                DragItem.Add("Image", this.bmpIcon);
                DragItem.Add("Action", new Action(delegate { this.Drag(new Action<DragCallback>(DragCallback)); }));
                DragItem.Add("ShortcutModifiers", this.shortCutDragModifiers);
                DragItem.Add("ShortcutKey", this.shortCutDragKey);
                ret.Add(DragItem);

                Hashtable UpItem = new Hashtable();
                UpItem.Add("Visible", Clipboard.ContainsImage() || Clipboard.ContainsFileDropList() || Clipboard.ContainsText());
                UpItem.Add("Text", "S3");
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
            Tray.Icon = new Icon("Addons/S3/Icon.ico");

            MemoryStream ms = new MemoryStream();

            ImageFormat format = ImageFormat.Png;
            switch (imageFormat.ToLower()) {
                case "png": format = ImageFormat.Png; break;
                case "jpg": format = ImageFormat.Jpeg; break;
                case "gif": format = ImageFormat.Gif; break;
            }

            img.Save(ms, format);
            img.Dispose();

            bool result = false;
            string failReason = "";
            string filename = "";

            bool canceled = false;
            try {
                filename = this.RandomFilename(8);
                filename = MD5(filename + rnd.Next(1000, 9999).ToString());
                filename += "." + imageFormat.ToLower();

                this.Backup(ms.GetBuffer(), filename);
                canceled = !UploadToAWS(ms, filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = GenerateURL(filename);
                    //this.AddLog(url);
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Image uploaded to AWS and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadAnimation(MemoryStream ms) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/S3/Icon.ico");

            bool result = false;
            string failReason = "";
            string filename = "";

            bool canceled = false;
            try {
                filename = this.RandomFilename(8);
                filename = MD5(filename + rnd.Next(1000, 9999).ToString());
                
                filename += ".gif";

                canceled = !UploadToAWS(ms, filename);

                this.Backup(ms.GetBuffer(), filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = GenerateURL(filename);
                    //this.AddLog(url);
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Animation uploaded to AWS and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadText(string Text) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/S3/Icon.ico");

            bool result = false;
            string failReason = "";
            string filename = "";

            byte[] textData = Encoding.UTF8.GetBytes(Text);

            bool canceled = false;
            try {
                filename = this.RandomFilename(8);
                filename = MD5(filename + rnd.Next(1000, 9999).ToString());

                filename += ".txt";

                canceled = !UploadToAWS(new MemoryStream(textData), filename);

                this.Backup(textData, filename);

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    string url = GenerateURL(filename);
                    //this.AddLog(url);
                    this.SetClipboardText(url);
                    Tray.ShowBalloonTip(1000, "Upload success!", "Text uploaded to AWS and URL copied to clipboard.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public void UploadFiles(StringCollection files) {
            Icon defIcon = (Icon)Tray.Icon.Clone();
            Tray.Icon = new Icon("Addons/S3/Icon.ico");

            bool result = false;
            string failReason = "";
            string finalCopy = "";

            bool canceled = false;

            try {
                foreach (string file in files) {
                    string filename = file.Split('/', '\\').Last();

                    canceled = !UploadToAWS(new MemoryStream(File.ReadAllBytes(file)), filename);
                    if (canceled)
                        break;

                    string url = GenerateURL( Uri.EscapeDataString(filename));
  
                    finalCopy += url + "\n";
                }

                result = true;
            } catch (Exception ex) { failReason = ex.Message; }

            if (!canceled) {
                if (result) {
                    this.SetClipboardText(finalCopy.Substring(0, finalCopy.Length - 1));
                    Tray.ShowBalloonTip(1000, "Upload success!", "File(s) uploaded to AmazonS3 and URL(s) copied.", ToolTipIcon.Info);
                } else {
                    this.ProgressBar.Done();
                    Tray.ShowBalloonTip(1000, "Upload failed!", "Something went wrong. Try again.\nMessage: '" + failReason + "'", ToolTipIcon.Error);
                }
            }

            Tray.Icon = defIcon;
        }

        public bool UploadToAWS(MemoryStream ms, string filename) {
            this.ProgressBar.Start(filename, 105);

            try {
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                request.UploadProgressEvent += displayProgress;
                request.AutoCloseStream = false;
                request.BucketName = bucketName;
                request.InputStream = ms;
                request.Key = prefix + filename;
                

                if (!isPrivate) 
                    request.AddHeader("x-amz-acl", "public-read");

                TransferUtility fileTransferUtility = new TransferUtility(AWSKey, AWSSecretKey);
                fileTransferUtility.Upload(request);
            }
            catch (Exception)
            {
                return false;
            }

            ms.Dispose();
            this.ProgressBar.Done();
            return true;
        }


        private void displayProgress(object sender, UploadProgressArgs args)
        {
            this.ProgressBar.Set(args.PercentDone);

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

        public string GenerateURL(string objectName)
        {
            AmazonS3 s3Client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSKey, AWSSecretKey);


            string url = "";
            try
            {
                using (s3Client)
                {
                    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
                    request.WithBucketName(bucketName);
                    request.WithKey(prefix + objectName);
                    if (expires == 0)
                    {
                        request.WithExpires(DateTime.Now.AddYears(20));
                    }
                    else
                    {
                        request.WithExpires(DateTime.Now.AddDays(expires));
                    }

                    
                    url = s3Client.GetPreSignedURL(request);
                }

            }
            catch (AmazonS3Exception)
            {
            }

            if (!isPrivate) {
                url = url.Substring(0, url.IndexOf("?")); //remove QS
            }

            if (!https) {
                url = url.Replace(@"https://", @"http://");
            }

            if (shorten ) {
                //create is.gd url
                string resp = "";
                try
                {
                    string isgd = @"http://is.gd/create.php?format=simple&url=" + System.Uri.EscapeDataString(url);
                    resp = new System.Net.WebClient().DownloadString(isgd);
                }
                catch (Exception) { }
                if (resp != "")
                {
                    url = resp;
                    if (appendExt)
                    {
                        if (System.IO.Path.GetExtension(objectName).ToLower() == "png")
                        {
                            url += "?" + System.IO.Path.GetExtension(objectName).ToLower();
                        }
                        else
                        {
                            url += "?" + System.IO.Path.GetFileName(objectName).ToLower();
                        }
                        
                    }
                }
                    
            }

                  
            return url;
        }

     
    }


}

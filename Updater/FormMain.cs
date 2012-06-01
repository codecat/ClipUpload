using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using SevenZip;
using System.Security.Cryptography;
using Updater.Properties;
using System.Diagnostics;

namespace Updater {
    public partial class FormMain : Form {
        public Settings appSettings;

        public string Version;
        public string DownloadURL;

        public FormMain() {
            InitializeComponent();

            this.appSettings = new Settings(Resources.Settings);
            this.Text = Resources.Product + " Updater";
        }

        public void AddLog(string str) {
            this.textInfo.Text += str;
            this.textInfo.SelectionStart = this.textInfo.Text.Length;
            this.textInfo.ScrollToCaret();

            Application.DoEvents();
        }

        public void AddLine(string str) {
            AddLog(str + "\r\n");
        }

        public long Epoch() {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadInfoCompleted);
            wc.DownloadStringAsync(new Uri(Resources.CheckURL));

            AddLine("Checking for updates...");
        }

        void DownloadInfoCompleted(object sender, DownloadStringCompletedEventArgs e) {
            if (e.Error != null) {
                AddLine("Couldn't check for updates. Please try again later!");
                return;
            }

            if (this.appSettings.GetString(Resources.SettingsKey) != e.Result) {
                this.Version = e.Result;
                AddLine("Version " + this.Version + " is available.");

                WebClient wc = new WebClient();
                wc.Proxy = null;
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadChangelogCompleted);
                wc.DownloadStringAsync(new Uri(Resources.ChangelogURL));
            } else
                AddLine("You are on " + e.Result + ", which is the latest version.");
        }

        void DownloadChangelogCompleted(object sender, DownloadStringCompletedEventArgs e) {
            string[] lines = e.Result.Split('\n');

            this.DownloadURL = lines[0].Split(new char[] { ':' }, 2)[1].Trim();

            if (this.DownloadURL.Contains("sourceforge.net/projects/")) {
                this.DownloadURL = this.DownloadURL.Replace("http://" + "sourceforge.net/projects/", "http://" + "downloads.sourceforge.net/project/");
                this.DownloadURL = this.DownloadURL.Substring(0, this.DownloadURL.Length - "/download".Length);
                this.DownloadURL = this.DownloadURL.Replace("/files/", "/");
                this.DownloadURL += "?r=&ts=" + this.Epoch();
            }

            for (int i = 1; i < lines.Length; i++) {
                string line = lines[i].Trim();

                if (line == "}")
                    break;

                if (line != "{" && line != "")
                    AddLine(" - " + line);
            }

            this.buttonUpdate.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e) {
            if (buttonUpdate.Text == "Update") {
                if (!File.Exists("Update" + this.Version + ".rar")) {
                    AddLine("Starting download...");
                    this.buttonUpdate.Enabled = false;

                    WebClient wc = new WebClient();
                    wc.Proxy = null;
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgress);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateDownloadCompleted);
                    wc.DownloadFileAsync(new Uri(this.DownloadURL), "Update" + this.Version + ".rar");
                } else
                    this.CompareFiles();
            } else {
                Process.Start(Resources.PostUpdate);
                this.Close();
            }
        }

        void UpdateDownloadProgress(object sender, DownloadProgressChangedEventArgs e) {
            this.progress.Value = e.ProgressPercentage;
        }

        void UpdateDownloadCompleted(object sender, AsyncCompletedEventArgs e) {
            this.progress.Value = 100;
            AddLine("Comparing files...");
            this.CompareFiles();
        }

        string RepeatString(string str, int times) {
            string ret = "";
            for (int i = 0; i < times; i++)
                ret += str;
            return ret;
        }

        void CompareFiles() {
            string rarfilename = "Update" + this.Version + ".rar";
            string path = Path.GetDirectoryName(Path.GetFullPath(rarfilename));
            SevenZipExtractor extr = new SevenZipExtractor(rarfilename);

            for (int i = 0; i < extr.ArchiveFileNames.Count; i++) {
                ArchiveFileInfo fileinfo = extr.ArchiveFileData[i];
                string filename = extr.ArchiveFileNames[i];
                bool updateFile = false;

                if (!fileinfo.IsDirectory) {
                    if (!File.Exists(filename)) {
                        updateFile = true;
                    } else {
                        if (Resources.Blacklist.Split(',').Contains(Path.GetFileName(filename)))
                            continue;

                        uint currentHash = Crc32.Compute(File.ReadAllBytes(filename));
                        uint updateHash = extr.ArchiveFileData[i].Crc;

                        if (currentHash != updateHash)
                            updateFile = true;
                    }
                } else {
                    if (!Directory.Exists(filename))
                        Directory.CreateDirectory(filename);
                    continue;
                }

                if (!updateFile)
                    continue;

                string pth = Path.GetDirectoryName(filename);
                if (pth != "") {
                    if (!Directory.Exists(pth))
                        Directory.CreateDirectory(pth);
                    if (File.Exists(filename))
                        File.Delete(filename);
                }

                AddLog("Updating " + filename + "...");

                FileStream fs = File.Create(filename);
                extr.ExtractFile(filename, fs);
                fs.Close();
                fs.Dispose();

                AddLine(RepeatString(" ", 73 - filename.Length) + " DONE");
            }

            if (bool.Parse(Resources.SettingsKeySet)) {
                appSettings.SetString("Version", this.Version);
                appSettings.Save();
            }

            extr.Dispose();

            if (File.Exists(rarfilename))
                File.Delete(rarfilename);

            buttonUpdate.Text = "Run " + Resources.Product;

            AddLine("Updating finished!");
        }
    }
}

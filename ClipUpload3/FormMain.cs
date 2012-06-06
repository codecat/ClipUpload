using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections;
using GlobalHook;
using GlobalHook.WinApi;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace ClipUpload3 {
    public partial class FormMain : Form {
        public class ShortcutInfo {
            public Action Action;
            public bool ModCtrl = false;
            public bool ModAlt = false;
            public bool ModShift = false;
            public Keys Key = 0;
        }

        public List<Addon> Addons = new List<Addon>();
        public List<ShortcutInfo> Shortcuts = new List<ShortcutInfo>();
        public Settings settings;

        bool mustExit = false;
        bool mustHide = false;
        string canUpdate = "";

        public string Version;

        public KeyboardHookListener keyboardListener;
        public KeyEventHandler keyboardHandler;

        public FormMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string path = Environment.CommandLine.Trim(' ', '"');

            if (!(path.Contains('\\') || path.Contains('/'))) {
                Console.WriteLine("Please start ClipUpload 3 from Explorer, or add the absolute path.");
                mustExit = true;
                this.Close();
            }

            string curDir = Directory.GetCurrentDirectory();

            Directory.SetCurrentDirectory(path.Substring(0, path.LastIndexOf('\\')));

            this.settings = new Settings("settings.txt");

            LoadSettings();
            LoadAddons();
            UpdateList();

            this.Text = "ClipUpload " + Version;

            if (this.settings.GetBool("CheckForUpdates")) {
                try {
                    WebClient wc = new WebClient();
                    wc.Proxy = null;
                    string latestVersion = wc.DownloadString("http://" + "clipupload.net/?update");
                    if (latestVersion != Version) {
                        this.canUpdate = latestVersion;
                        this.Tray.ShowBalloonTip(10, "ClipUpload Update", "A new update for ClipUpload is available, version " + latestVersion + ". Click \"Update to " + latestVersion + "\" in the ClipUpload menu to automatically update.", ToolTipIcon.Info);
                    }
                } catch (Exception ex) {
                    Program.Debug("Update check threw " + ex.GetType().FullName + ": '" + ex.Message + "'");
                }
            }

            if (curDir != Directory.GetCurrentDirectory())
                mustHide = true;

            keyboardHandler = new KeyEventHandler(keyboardListener_KeyDown);

            this.keyboardListener = new KeyboardHookListener(new GlobalHooker());
            this.keyboardListener.Enabled = true;
            this.keyboardListener.KeyDown += keyboardHandler;
        }

        public void LoadSettings() {
            if (this.settings.GetString("Version") == "3.00") {
                // Migrate from old 3.00 config file
                this.settings.SetString("Version", "3.01");
                this.settings.SetBool("ProgressBar", true);
                this.settings.SetBool("PortableProgressBar", false);

                this.settings.Save();
            }

            if (this.settings.GetString("Version") == "3.01") {
                // Migrate from old 3.01 config file
                this.settings.SetString("Version", "3.02");
                this.settings.SetBool("ProxyEnabled", false);
                this.settings.SetString("ProxyHost", "");
                this.settings.SetInt("ProxyPort", 8080);
                this.settings.SetString("ProxyUsername", "");
                this.settings.SetString("ProxyPassword", "");

                this.settings.Save();
            }

            if (this.settings.GetString("Version") == "3.02") {
                // Migrate from old 3.02 config file
                this.settings.SetString("Version", "3.03");
                this.settings.SetBool("DragExtra", false);
                this.settings.SetString("DragExtraName", "Paint");
                this.settings.SetString("DragExtraPath", "C:\\Windows\\system32\\mspaint.exe");

                this.settings.Save();
            }

            if (this.settings.GetString("Version") == "3.03") {
                // Migrate from old 3.03 config file
                this.settings.SetString("Version", "3.04");

                this.settings.Save();
            }

            if (this.settings.GetString("Version") == "3.04") {
                // Migrate from old 3.04 config file
                this.settings.SetString("Version", "3.10");
                this.settings.SetInt("DragEditor", 1);
                this.settings.SetInt("DragAnimFPS", 10);
                this.settings.SetBool("BackupsEnabled", false);
                this.settings.SetString("BackupsPath", "Backup");
                this.settings.SetString("BackupsFormat", "%DATE% %TIME% %FILENAME%");

                this.settings.Save();
            }

            this.Version = this.settings.GetString("Version");
            this.panelDonate.Visible = this.settings.GetBool("DonateVisible");
        }

        public void LoadShortcuts() {
            this.Shortcuts.Clear();
            foreach (Addon addon in Addons) {
                Hashtable[] MenuItems = addon.CallHook2<Hashtable[]>("Menu", new Hashtable[0]);
                foreach (Hashtable MenuItem in MenuItems) {
                    if (MenuItem.ContainsKey("ShortcutModifiers") && MenuItem.Contains("ShortcutKey")) {
                        Hashtable temp = (Hashtable)MenuItem.Clone();

                        string[] parts = ((string)temp["ShortcutModifiers"]).Split('+');
                        bool reqCtrl = false, reqAlt = false, reqShift = false;
                        foreach (string part in parts) {
                            switch (part.ToLower().Trim()) {
                                case "ctrl": reqCtrl = true; break;
                                case "alt": reqAlt = true; break;
                                case "shift": reqShift = true; break;
                            }
                        }

                        string shortCutKey = (string)temp["ShortcutKey"];
                        if (new List<string>(Enum.GetNames(typeof(Keys)).AsEnumerable<string>()).Contains(shortCutKey)) {
                            Keys reqKey = (Keys)typeof(Keys).GetField(shortCutKey).GetValue(null);
                            this.Shortcuts.Add(new ShortcutInfo() {
                                Action = (Action)temp["Action"],
                                ModCtrl = reqCtrl,
                                ModAlt = reqAlt,
                                ModShift = reqShift,
                                Key = reqKey
                            });
                        }
                    }
                }
            }
        }

        public void SaveAddons() {
            foreach (Addon addon in Addons)
                addon.Settings.Save();
            button1.Enabled = false;

            LoadShortcuts();
        }

        public void LoadAddons() {
            string[] dirs = Directory.GetDirectories("Addons");
            Addons.Clear();
            foreach (string dir in dirs) {
                Addon addon = new Addon(dir);
                if (addon.Settings.GetBool("Enabled")) {
                    addon.LoadAssembly();
                    addon.CallHook("Initialize", Tray);
                }
                Addons.Add(addon);
            }

            LoadShortcuts();
        }

        public void UpdateList() {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(16, 16);
            imgList.ColorDepth = ColorDepth.Depth32Bit;

            listAddons.Items.Clear();

            int c = 0;
            foreach (Addon addon in Addons) {
                string Icon = addon.Directory + "\\" + addon.Settings.GetString("Icon");
                if (Icon.EndsWith(".ico")) {
                    try {
                        imgList.Images.Add(new Icon(Icon));
                    } catch { imgList.Images.Add(Image.FromFile(Icon)); }
                } else
                    imgList.Images.Add(Image.FromFile(Icon));

                ListViewItem lvi = listAddons.Items.Add(addon.Settings.GetString("Name"));
                lvi.SubItems.Add(addon.Settings.GetString("Author"));
                lvi.ImageIndex = c;
                lvi.UseItemStyleForSubItems = false;

                ListViewItem.ListViewSubItem enabled = lvi.SubItems.Add(addon.Settings.GetBool("Enabled") ? "Yes" : "No");
                enabled.ForeColor = addon.Settings.GetBool("Enabled") ? Color.Green : Color.Red;
                enabled.Font = new Font(this.Font, FontStyle.Bold);

                c++;
            }

            listAddons.SmallImageList = imgList;
        }

        private void button4_Click(object sender, EventArgs e) {
            KillMe();
        }

        public void KillMe() {
            mustExit = true;
            this.Close();
        }

        private void listAddons_MouseUp(object sender, MouseEventArgs e) {
            if (listAddons.SelectedItems.Count == 1 && e.Button == MouseButtons.Right) {
                Addon addon = Addons[listAddons.SelectedItems[0].Index];

                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripItem tsi;

                tsi = cms.Items.Add(addon.Settings.GetBool("Enabled") ? "Disable" : "Enable");
                tsi.Image = addon.Settings.GetBool("Enabled") ? this.iconList.Images[2] : this.iconList.Images[3];
                tsi.Click += new EventHandler(delegate {
                    addon.Settings.SetBool("Enabled", !addon.Settings.GetBool("Enabled"));

                    if (addon.Settings.GetBool("Enabled")) {
                        addon.LoadAssembly();
                        addon.CallHook("Initialize", this.Tray);
                    } else {
                        addon.CallHook("Uninitialize");
                        addon.Assembly = null;
                    }

                    UpdateList();
                    button1.Enabled = true;
                });

                if (addon.Settings.GetBool("Enabled")) {
                    tsi = cms.Items.Add("Settings");
                    tsi.Image = this.iconList.Images[0];
                    tsi.Click += new EventHandler(delegate {
                        addon.CallHook("Settings");
                    });
                }

                /*tsi = cms.Items.Add("Remove");
                tsi.Click += new EventHandler(delegate
                {
                    addon.CallHook("Removed");

                    addon.Assembly = null;
                    Addons.Remove(addon);
                });*/

                listAddons.ContextMenuStrip = cms;
            }
        }

        private void listAddons_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (listAddons.SelectedItems.Count == 1) {
                Addon addon = Addons[listAddons.SelectedItems[0].Index];
                if (addon.Settings.GetBool("Enabled")) {
                    addon.CallHook("Settings");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            UpdateList();

            Hide();
        }

        private void button3_Click(object sender, EventArgs e) {
            SaveAddons();

            Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
            SaveAddons();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (!mustExit && e.CloseReason != CloseReason.WindowsShutDown) {
                e.Cancel = true;
                Hide();
            } else {
                foreach (Addon addon in Addons)
                    addon.CallHook("Uninitialize");
            }
        }

        void keyboardListener_KeyDown(object sender, KeyEventArgs e) {
            bool control = ModifierKeys.HasFlag(Keys.Control);
            bool shift = ModifierKeys.HasFlag(Keys.Shift);
            bool alt = ModifierKeys.HasFlag(Keys.Alt);

            foreach (ShortcutInfo shortcut in this.Shortcuts) {
                bool modifiersOK = (control || !shortcut.ModCtrl) &&
                                   (shift || !shortcut.ModShift) &&
                                   (alt || !shortcut.ModAlt);
                if (modifiersOK && e.KeyCode == shortcut.Key)
                    shortcut.Action();
            }
        }

        private void Tray_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                GC.Collect();

                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem tsi;

                foreach (Addon addon in Addons) {
                    int MenuItemsAdded = 0;

                    Hashtable[] MenuItems = addon.CallHook2<Hashtable[]>("Menu", new Hashtable[0]);
                    for (int i = 0; i < MenuItems.Length; i++) {
                        if ((bool)MenuItems[i]["Visible"]) {
                            tsi = (ToolStripMenuItem)cms.Items.Add((string)MenuItems[i]["Text"]);
                            tsi.Image = (Image)MenuItems[i]["Image"];

                            Action temp = (MenuItems[i]["Action"] as Action);
                            tsi.Click += new EventHandler(delegate {
                                temp.Invoke();
                            });

                            if (MenuItems[i].ContainsKey("ShortcutModifiers") && MenuItems[i].Contains("ShortcutKey"))
                                tsi.ShortcutKeyDisplayString = ((string)MenuItems[i]["ShortcutModifiers"] + "+" + (string)MenuItems[i]["ShortcutKey"]).Trim('+');

                            MenuItemsAdded++;
                        }
                    }

                    if (MenuItemsAdded > 0)
                        cms.Items.Add(new ToolStripSeparator());
                }

                tsi = (ToolStripMenuItem)cms.Items.Add("Settings");
                tsi.Image = this.iconList.Images[0];
                tsi.Click += new EventHandler(delegate { Show(); });

                tsi = (ToolStripMenuItem)cms.Items.Add("Upload Log");
                tsi.Image = this.iconList.Images[1];
                tsi.Click += new EventHandler(delegate { button7_Click(null, null); });

                if (settings.GetBool("BackupsEnabled")) {
                    tsi = (ToolStripMenuItem)cms.Items.Add("Backups");
                    tsi.Image = this.iconList.Images[4];
                    tsi.Click += new EventHandler(delegate {
                        Process.Start("explorer", "\"" + Path.GetFullPath(settings.GetString("BackupsPath")) + "\"");
                    });
                }

                if (this.canUpdate != "") {
                    tsi = (ToolStripMenuItem)cms.Items.Add("Update to " + this.canUpdate);
                    tsi.Image = this.iconList.Images[5];
                    tsi.Click += new EventHandler(delegate {
                        if (File.Exists("Updater.exe")) {
                            Process.Start("Updater.exe");
                            this.KillMe();
                        }
                    });
                }

                tsi = (ToolStripMenuItem)cms.Items.Add("Exit");
                tsi.Click += new EventHandler(delegate { KillMe(); });

                Tray.ContextMenuStrip = cms;
            }
        }

        private void Tray_BalloonTipClicked(object sender, EventArgs e) {
            // When the notify balloon tip is shown, this will always be called when the icon is clicked,
            // which cancels out the above event. Thus, we redirect this one in that case to the above method.
            Tray_MouseDown(null, new MouseEventArgs(MouseButtons.Right, 0, 0, 0, 0));
        }

        private void Tray_DoubleClick(object sender, EventArgs e) {
            Show();
        }

        private void button5_Click(object sender, EventArgs e) {
            new FormSettings(this).ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e) {
            if (mustHide) {
                Hide();
                mustHide = false;
            }
        }

        private void button6_Click(object sender, EventArgs e) {
            Process.Start(settings.GetString("AddonsURL"));
        }

        private void button7_Click(object sender, EventArgs e) {
            new FormUploadLog().ShowDialog();
        }

        private void pictureDonate_Click(object sender, EventArgs e) {
            Process.Start("http://" + "sourceforge.net/donate/index.php?group_id=340379");
        }
    }
}

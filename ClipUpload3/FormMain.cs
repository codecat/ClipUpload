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

        public string Version;

        KeyboardHookListener keyboardListener;

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
                    if (latestVersion != Version)
                        this.Tray.ShowBalloonTip(10, "ClipUpload Update", "A new update for ClipUpload is available, version " + latestVersion + ". Visit http://" + "clipupload.net/ to download the latest version!", ToolTipIcon.Info);
                } catch { }
            }

            if (curDir != Directory.GetCurrentDirectory())
                mustHide = true;

            this.keyboardListener = new KeyboardHookListener(new GlobalHooker());
            this.keyboardListener.Enabled = true;
            this.keyboardListener.KeyDown += new KeyEventHandler(keyboardListener_KeyDown);
        }

        public void LoadSettings() {
            if (!this.settings.Contains("ProgressBar")) {
                // Migrate from old 3.00 config file
                this.settings.SetBool("ProgressBar", true);
                this.settings.SetBool("PortableProgressBar", false);

                this.settings.Save();
            }

            if (!this.settings.Contains("ProxyEnabled")) {
                // Migrate from old 3.01 config file
                this.settings.SetBool("ProxyEnabled", false);
                this.settings.SetString("ProxyHost", "");
                this.settings.SetInt("ProxyPort", 8080);
                this.settings.SetString("ProxyUsername", "");
                this.settings.SetString("ProxyPassword", "");

                this.settings.Save();
            }

            if (!this.settings.Contains("DragExtra")) {
                // Migrate from old 3.02 config file
                this.settings.SetBool("DragExtra", false);
                this.settings.SetString("DragExtraName", "Paint");
                this.settings.SetString("DragExtraPath", "C:\\Windows\\system32\\mspaint.exe");

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
                if (Icon.EndsWith(".ico"))
                    imgList.Images.Add(new Icon(Icon));
                else
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
                    shortcut.Action.Invoke();
            }
        }

        private void Tray_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem tsi;

                foreach (Addon addon in Addons) {
                    int MenuItemsAdded = 0;

                    Hashtable[] MenuItems = addon.CallHook2<Hashtable[]>("Menu", new Hashtable[0]);
                    foreach (Hashtable MenuItem in MenuItems) {
                        if ((bool)MenuItem["Visible"]) {
                            tsi = (ToolStripMenuItem)cms.Items.Add((string)MenuItem["Text"]);
                            tsi.Image = (Image)MenuItem["Image"];
                            Hashtable temp = (Hashtable)MenuItem.Clone();
                            tsi.Click += new EventHandler(delegate {
                                ((Action)temp["Action"]).Invoke();
                            });
                            if (MenuItem.ContainsKey("ShortcutModifiers") && MenuItem.Contains("ShortcutKey"))
                                tsi.ShortcutKeyDisplayString = ((string)MenuItem["ShortcutModifiers"] + "+" + (string)MenuItem["ShortcutKey"]).Trim('+');

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

                tsi = (ToolStripMenuItem)cms.Items.Add("Exit");
                tsi.Click += new EventHandler(delegate { KillMe(); });

                Tray.ContextMenuStrip = cms;
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using AddonHelper;

namespace TestAddon {
    public class TestAddon : Addon {
        public NotifyIcon Tray;

        public void Initialize(NotifyIcon Tray) {
            this.Tray = Tray;
        }

        public Hashtable[] Menu() {
            List<Hashtable> ret = new List<Hashtable>();

            Hashtable startProgressBar = new Hashtable();
            startProgressBar.Add("Visible", true);
            startProgressBar.Add("Text", "Copy Text");
            startProgressBar.Add("Action", new Action(delegate {
                this.SetClipboardText("Hello, world!");
            }));
            ret.Add(startProgressBar);

            return ret.ToArray();
        }

        public void Settings() {
            MessageBox.Show("No settings for a Test addon, silly...", "Test Addon", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

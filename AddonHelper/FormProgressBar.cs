using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AddonHelper {
    public partial class FormProgressBar : Form {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        protected override bool ShowWithoutActivation {
            get { return true; }
        }

        public bool DwmEnabled {
            get {
                if (Environment.OSVersion.Version.Major >= 6) {
                    return DwmIsCompositionEnabled();
                } else {
                    return false;
                }
            }
        }

        public FormProgressBar(Settings settings) {
            InitializeComponent();

            if (settings.GetBool("PortableProgressBar")) {
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            if (this.DwmEnabled) {
                this.Left = workingArea.Width - this.Width - 4;
                this.Top = workingArea.Height - this.Height - 4;
            } else {
                this.Left = workingArea.Width - this.Width;
                this.Top = workingArea.Height - this.Height;
            }
        }
    }
}

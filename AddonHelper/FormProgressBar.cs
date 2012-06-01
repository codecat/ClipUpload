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

        public bool DwmEnabled {
            get {
                if (Environment.OSVersion.Version.Major >= 6)
                    return DwmIsCompositionEnabled();
                else
                    return false;
            }
        }

        protected override bool ShowWithoutActivation {
            get { return true; } // Since this form is TopMost, we need the below shown CreateParams override as well
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams p = base.CreateParams;
                p.ExStyle |= 8; // WS_EX_TOPMOST
                return p;
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

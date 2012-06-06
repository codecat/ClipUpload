using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ClipUpload3 {
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        public static void Debug(string str) {
            StreamWriter writer;
            if (File.Exists("debug.txt"))
                writer = File.AppendText("debug.txt");
            else
                writer = new StreamWriter(File.Create("debug.txt"));

            writer.WriteLine("[ClipUpload3 - " + DateTime.Now.ToString() + "] " + str);
            writer.Close();
            writer.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snaggy {
    public partial class FormSettings : Form {
        Snaggy mainClass;

        public FormSettings(Snaggy mainClass) {
            InitializeComponent();

            this.mainClass = mainClass;

            int selIndex = 0;
            switch (mainClass.imageFormat.ToLower()) {
                case "png": selIndex = 0; break;
                case "jpg": selIndex = 1; break;
                case "gif": selIndex = 2; break;
            }
            comboFormat.SelectedIndex = selIndex;

            {
                string[] parts = mainClass.shortCutDragModifiers.Split('+');
                foreach (string part in parts) {
                    switch (part) {
                        case "Ctrl": checkDragModCtrl.Checked = true; break;
                        case "Alt": checkDragModAlt.Checked = true; break;
                        case "Shift": checkDragModShift.Checked = true; break;
                    }
                }
            }

            {
                string[] parts = mainClass.shortCutPasteModifiers.Split('+');
                foreach (string part in parts) {
                    switch (part) {
                        case "Ctrl": checkPasteModCtrl.Checked = true; break;
                        case "Alt": checkPasteModAlt.Checked = true; break;
                        case "Shift": checkPasteModShift.Checked = true; break;
                    }
                }
            }

            mainClass.PopulateKeysCombobox(comboDragKeys);
            mainClass.PopulateKeysCombobox(comboPasteKeys);

            comboDragKeys.SelectedItem = mainClass.shortCutDragKey;
            comboPasteKeys.SelectedItem = mainClass.shortCutPasteKey;
        }

        private void button1_Click(object sender, EventArgs e) {
            mainClass.imageFormat = comboFormat.Items[comboFormat.SelectedIndex].ToString();
            mainClass.settings.SetString("Format", comboFormat.Items[comboFormat.SelectedIndex].ToString());

            {
                string shortcutModifiers = "";
                if (checkDragModCtrl.Checked) shortcutModifiers += "+Ctrl";
                if (checkDragModAlt.Checked) shortcutModifiers += "+Alt";
                if (checkDragModShift.Checked) shortcutModifiers += "+Shift";
                shortcutModifiers = shortcutModifiers.Trim('+');

                mainClass.settings.SetString("ShortcutDragModifiers", shortcutModifiers);
                mainClass.settings.SetString("ShortcutDragKey", (string)comboDragKeys.SelectedItem != "None" ? (string)comboDragKeys.SelectedItem : "");
            }

            {
                string shortcutModifiers = "";
                if (checkPasteModCtrl.Checked) shortcutModifiers += "+Ctrl";
                if (checkPasteModAlt.Checked) shortcutModifiers += "+Alt";
                if (checkPasteModShift.Checked) shortcutModifiers += "+Shift";
                shortcutModifiers = shortcutModifiers.Trim('+');

                mainClass.settings.SetString("ShortcutPasteModifiers", shortcutModifiers);
                mainClass.settings.SetString("ShortcutPasteKey", (string)comboPasteKeys.SelectedItem != "None" ? (string)comboPasteKeys.SelectedItem : "");
            }

            mainClass.settings.Save();

            mainClass.LoadSettings();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}

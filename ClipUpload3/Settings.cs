using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace ClipUpload3 {
    public class Settings {
        private Dictionary<string, string> Keys = new Dictionary<string, string>();
        private string Filename;

        public Settings(String Filename) {
            this.Filename = Filename;

            loadFile();
        }

        private void loadFile() {
            if (File.Exists(Filename)) {
                string[] lines = File.ReadAllLines(Filename);
                string[] parse;
                Keys.Clear();
                foreach (string line in lines) {
                    parse = line.Split(new char[] { '=' }, 2);
                    if (parse.Length == 2)
                        Keys.Add(parse[0], parse[1]);
                }
            }
        }

        public string GetFilename() {
            return Filename;
        }

        public string GetString(string key) {
            if (Keys.ContainsKey(key))
                return Keys[key];
            return "";
        }
        public bool GetBool(string key) {
            try {
                if (Keys.ContainsKey(key))
                    return bool.Parse(Keys[key]);
            } catch { }
            return false;
        }
        public int GetInt(string key) {
            try {
                if (Keys.ContainsKey(key))
                    return int.Parse(Keys[key]);
            } catch { }
            return 0;
        }
        public float GetFloat(string key) {
            try {
                if (Keys.ContainsKey(key))
                    return float.Parse(Keys[key]);
            } catch { }
            return 0f;
        }

        public bool Contains(string key) {
            return Keys.ContainsKey(key);
        }

        public void SetString(string key, string value) {
            Keys[key] = value;
        }
        public void SetBool(string key, bool value) {
            Keys[key] = value.ToString();
        }
        public void SetInt(string key, int value) {
            Keys[key] = value.ToString();
        }
        public void SetFloat(string key, float value) {
            Keys[key] = value.ToString();
        }

        public void Delete(string key) {
            if (Keys.ContainsKey(key))
                Keys.Remove(key);
        }

        public void Save() {
            string[] output = new string[Keys.Count];
            int c = 0;
            foreach (KeyValuePair<string, string> entry in Keys) {
                output[c] = entry.Key + "=" + entry.Value;
                c++;
            }
            File.WriteAllLines(Filename, output);
        }

        public void Cancel() {
            loadFile();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Gett {
    public class GettAPIRequest {
        private Dictionary<string, string> apiValues = new Dictionary<string, string>();
        private Gett mainClass;

        public string Action = "";

        public GettAPIRequest(Gett mainClass, string action) {
            this.mainClass = mainClass;
            this.Action = action;
        }

        private bool _APIKeyRequired = false;
        public bool APIKeyRequired {
            get { return _APIKeyRequired; }
            set {
                if (value) {
                    this.apiValues["apikey"] = this.mainClass.APIKey;
                } else {
                    if (this.apiValues.ContainsKey("apikey")) {
                        this.apiValues.Remove("apikey");
                    }
                }
                _APIKeyRequired = value;
            }
        }

        public void Add(string key, string value) {
            this.apiValues[key] = value;
        }

        private string Data {
            get {
                string ret = "{";
                foreach (string key in this.apiValues.Keys) {
                    ret += "\"" + Uri.EscapeDataString(key) + "\":\"" + Uri.EscapeDataString(this.apiValues[key]) + "\"";
                    if (this.apiValues.Last().Key != key) {
                        ret += ",";
                    }
                }
                return ret + "}";
            }
        }

        public object Execute() {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //try {
                string result = wc.UploadString(this.mainClass.BaseURL + this.Action, this.Data);
                return JSON.JsonDecode(result);
            //} catch {
            //    return null;
            //}
        }
    }
}

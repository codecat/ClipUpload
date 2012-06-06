using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

// TODO: This entire library is using signature method PLAINTEXT. That's okay for https requests, but for http requests HMAC-SHA1 is required. See oauth.net/core/1.0a for more info.

namespace MrAG.OAuth {
    /// <summary>
    /// Provices basic OAuth 1.0a functionality
    /// </summary>
    public class OAuth {
        private string _EndPointURL;

        /// <summary>
        /// Gets or sets the OAuth endpoint URL
        /// </summary>
        public string EndPointURL {
            get { return this._EndPointURL; }
            set {
                this._EndPointURL = value;
                if (this._EndPointURL.EndsWith("/"))
                    this._EndPointURL = value.Substring(0, value.Length - 1);
            }
        }

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets the OAuth access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets the OAuth access token secret
        /// </summary>
        public string AccessTokenSecret { get; set; }

        /// <summary>
        /// Gets the OAuth token
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Gets the OAuth token secret
        /// </summary>
        public string TokenSecret { get; private set; }

        /// <summary>
        /// Gets or sets the icon used for the authorization form. If no icon is provided, the form will have a default icon.
        /// </summary>
        public Icon ServiceIcon { get; set; }

        /// <summary>
        /// Gets or sets the service name used for the authorization form. If no name is provided, the form will have a default title.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Create an OAuth object with the given endpoint URL
        /// </summary>
        /// <param name="endpoint">The endpoint URL</param>
        public OAuth(string endpoint, string consumerKey, string consumerSecret) {
            this.EndPointURL = endpoint;
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        private Random rnd = new Random();
        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="len">Length of the random string</param>
        /// <param name="chars">Characters used to generate the string</param>
        /// <returns>The generated random string based on length and characters</returns>
        private string randomString(int len, string chars = "abcdefghijklmnopqrstuvwxyz0123456789") {
            string ret = "";
            for (int i = 0; i < len; i++)
                ret += chars[rnd.Next(chars.Length)].ToString();
            return ret;
        }

        /// <summary>
        /// Short hand alias for Uri.EscapeDataString
        /// </summary>
        /// <param name="str">The string to escape</param>
        /// <returns>An escaped string</returns>
        private string eds(string str) {
            return Uri.EscapeDataString(str);
        }

        /// <summary>
        /// Get the current epoch time
        /// </summary>
        /// <returns>The current epoch time</returns>
        private long epoch() {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// Parses URI scheme data params
        /// </summary>
        /// <param name="input">The data string</param>
        /// <returns>Dictionary with parsed data</returns>
        private Dictionary<string, string> parseDataParams(string input) {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            string currentName = "";
            string currentValue = "";
            bool readingValue = false;

            for (int i = 0; i < input.Length; i++) {
                switch (input[i]) {
                    case '=':
                        readingValue = true;
                        break;

                    case '&':
                        ret[currentName] = Uri.UnescapeDataString(currentValue);
                        currentName = "";
                        currentValue = "";
                        readingValue = false;
                        break;

                    default:
                        if (!readingValue)
                            currentName += input[i];
                        else
                            currentValue += input[i];
                        break;
                }
            }

            if (currentName != "" && currentValue != "")
                ret[currentName] = Uri.UnescapeDataString(currentValue);

            return ret;
        }

        /// <summary>
        /// Internal function to download a URL with a maximum of 4 retries
        /// </summary>
        /// <param name="url">The URL</param>
        /// <returns>The result</returns>
        private string downloadURL(string url) {
            WebClient wc = new WebClient() { Proxy = null };
            for (int i = 0; i < 4; i++)
                try { return wc.DownloadString(url); } catch { }
            return "";
        }

        /// <summary>
        /// Get the user authorization URL
        /// </summary>
        /// <returns>The URL for user authorization</returns>
        public string GetAuthorizeURL(string callback) {
            string requestTokenParams = "OAuth realm=\"" + this.EndPointURL + "\"";
            requestTokenParams += ",oauth_consumer_key=\"" + eds(this.ConsumerKey) + "\"";
            requestTokenParams += ",oauth_signature_method=\"PLAINTEXT\"";
            requestTokenParams += ",oauth_signature=\"" + eds(this.ConsumerSecret + "&") + "\"";
            requestTokenParams += ",oauth_timestamp=\"" + epoch() + "\"";
            requestTokenParams += ",oauth_nonce=\"" + randomString(16) + "\"";
            requestTokenParams += ",oauth_callback=\"" + callback + "\"";
            requestTokenParams += ",oauth_version=\"1.0\"";

            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.Headers[HttpRequestHeader.UserAgent] = "MrAG.OAuth";
            wc.Headers[HttpRequestHeader.Authorization] = requestTokenParams;

            string resultString = "";
            try {
                resultString = wc.UploadString(this.EndPointURL + "/request_token", "");
            } catch { }

            if (resultString != "") {
                Dictionary<string, string> result = parseDataParams(resultString);
                if (result.ContainsKey("oauth_callback_accepted") && result["oauth_callback_accepted"] == "1" || result["oauth_callback_accepted"].ToLower() == "true") {
                    this.Token = result["oauth_token"];
                    this.TokenSecret = result["oauth_token_secret"];

                    return this.EndPointURL + "/authorize?oauth_token=" + this.Token;
                }
            }

            return "";
        }

        /// <summary>
        /// Get the final access token
        /// </summary>
        /// <param name="callbackURL">The callback url with resulting URL parameters</param>
        /// <returns>The access token</returns>
        public string GetAccessToken(string callbackURL) {
            Dictionary<string, string> urlParams = parseDataParams(callbackURL.Split(new char[] { '?' }, 2).Last());
            if (urlParams.ContainsKey("oauth_verifier")) {
                string verifier = urlParams["oauth_verifier"];

                string accessTokenURL = "/access_token";
                accessTokenURL += "?oauth_consumer_key=" + eds(this.ConsumerKey);
                accessTokenURL += "&oauth_token=" + eds(this.Token);
                accessTokenURL += "&oauth_signature_method=PLAINTEXT";
                accessTokenURL += "&oauth_signature=" + eds(this.ConsumerSecret + "&" + this.TokenSecret);
                accessTokenURL += "&oauth_timestamp=" + epoch();
                accessTokenURL += "&oauth_nonce=" + randomString(16);
                accessTokenURL += "&oauth_verifier=" + eds(verifier);
                accessTokenURL += "&oauth_version=1.0";

                Dictionary<string, string> result = parseDataParams(downloadURL(this.EndPointURL + accessTokenURL));
                if (result.ContainsKey("oauth_token")) {
                    this.AccessToken = result["oauth_token"];
                    this.AccessTokenSecret = result["oauth_token_secret"];

                    return this.AccessToken;
                }
            }

            return "";
        }

        /// <summary>
        /// Authorize automatically using an authorization form
        /// </summary>
        /// <returns>The access token</returns>
        public string Authorize() {
            OAuthForm newForm = new OAuthForm(this, this.EndPointURL + "/?finished");
            newForm.ShowDialog();
            return this.AccessToken;
        }

        /// <summary>
        /// Authorize automatically using an authorization form
        /// </summary>
        /// <param name="owner">The owner form</param>
        /// <returns>The access token</returns>
        public string Authorize(IWin32Window formOwner) {
            OAuthForm newForm = new OAuthForm(this, this.EndPointURL + "/?finished");
            newForm.ShowDialog(formOwner);
            return this.AccessToken;
        }

        private string authString {
            get {
                string auth = "OAuth realm=\"" + eds(this.EndPointURL) + "\"";
                auth += ",oauth_consumer_key=\"" + eds(this.ConsumerKey) + "\"";
                auth += ",oauth_token=\"" + eds(this.AccessToken) + "\"";
                auth += ",oauth_signature_method=\"PLAINTEXT\"";
                auth += ",oauth_signature=\"" + eds(this.ConsumerSecret + "&" + this.AccessTokenSecret) + "\"";
                auth += ",oauth_timestamp=\"" + epoch() + "\"";
                auth += ",oauth_nonce=\"" + randomString(16) + "\"";
                auth += ",oauth_version=\"1.0\"";
                return auth;
            }
        }

        /// <summary>
        /// Get an authenticated WebClient object to use for requests
        /// </summary>
        /// <returns>An authenticated WebClient object</returns>
        public WebClient AuthenticatedWebClient() {
            WebClient wc = new WebClient();
            wc.Proxy = null;
            wc.Headers[HttpRequestHeader.UserAgent] = "MrAG.OAuth";
            wc.Headers[HttpRequestHeader.Authorization] = authString;

            return wc;
        }

        /// <summary>
        /// Get an authenticated HttpWebRequest object to use for requests
        /// </summary>
        /// <param name="url">The base URL</param>
        /// <returns>An authenticated HttpWebRequest object</returns>
        public HttpWebRequest AuthenticatedWebRequest(string url) {
            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(url);
            wr.Proxy = null;
            wr.UserAgent = "MrAG.OAuth";
            wr.Headers[HttpRequestHeader.Authorization] = authString;

            return wr;
        }
    }
}

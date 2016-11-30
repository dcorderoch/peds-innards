using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace MyLearn.GoogleService
{
    public class AuthResponse
    {
        private static string _MyLearnClientId = "530714208118-8n2relpdv57rtt08c359hbiq3ej4jt1r.apps.googleusercontent.com";
        private static string _MyLearnClientSecret = "_8R6dhhuwXRdDJ3kxghnuct1";

        private string access_token;
        public string refresh_token { get; set; }
        public string clientId { get; set; }
        public string secret { get; set; }
        public DateTime created { get; set; }
        public string Access_token
        {
            get
            {
                // Access token lasts an hour if its expired we get a new one.
                if (DateTime.Now.Subtract(created).Hours > 1)
                {
                    refresh();
                }
                return access_token;
            }
            set
            {
                access_token = value;
            }
        }


        public AuthResponse() { }
        public AuthResponse(string previouslyObtainedRefreshToken)
        {
            string rtoken = previouslyObtainedRefreshToken;
            this.Access_token = ""; //to avoid null exceptions
            this.refresh_token = rtoken;
            this.created = DateTime.Now.Add(new TimeSpan(-2, 0, 0));
            this.clientId = _MyLearnClientId;
            this.secret = _MyLearnClientSecret;
        }

        /// <summary>
        /// Parse the json response 
        /// //  "{\n  \"access_token\" : \"ya29.kwFUj-la2lATSkrqFlJXBqQjCIZiTg51GYpKt8Me8AJO5JWf0Sx6-0ZWmTpxJjrBrxNS_JzVw969LA\",\n  \"token_type\" : \"Bearer\",\n  \"expires_in\" : 3600,\n  \"refresh_token\" : \"1/ejoPJIyBAhPHRXQ7pHLxJX2VfDBRz29hqS_i5DuC1cQ\"\n}"
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static AuthResponse get(string response)
        {
            Console.WriteLine(response);
            AuthResponse result = JsonConvert.DeserializeObject<AuthResponse>(response);
            result.created = DateTime.Now;   // DateTime.Now.Add(new TimeSpan(-2, 0, 0)); //For testing force refresh.
            return result;
        }

        public static AuthResponse fromRefresh(string refreshToken)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            string postData = string.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token", _MyLearnClientId, _MyLearnClientSecret, refreshToken);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var refreshResponse = AuthResponse.get(responseString);

            if (refreshResponse.refresh_token == null)
            {
                refreshResponse.refresh_token = refreshToken;
            }

            return refreshResponse;
        }

        public void refresh()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            string postData = string.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token", this.clientId, this.secret, this.refresh_token);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var refreshResponse = AuthResponse.get(responseString);
            this.access_token = refreshResponse.access_token;
            this.refresh_token = (refreshResponse.refresh_token == null) ? this.refresh_token : refreshResponse.refresh_token;
            this.created = DateTime.Now;
        }


        public static AuthResponse Exchange(string authCode, string clientid, string secret, string redirectURI)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");

            string postData = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", authCode, clientid, secret, redirectURI);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var x = AuthResponse.get(responseString);

                x.clientId = clientid;
                x.secret = secret;
                return x;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Uri GetAutenticationURI(string clientId, string redirectUri)
        {
            string scopes = "https://www.googleapis.com/auth/drive.file";

            if (string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = "urn:ietf:wg:oauth:2.0:oob";
            }
            string oauth = string.Format("https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&scope={2}&response_type=code", clientId, redirectUri, scopes);
            return new Uri(oauth);
        }
    }
}
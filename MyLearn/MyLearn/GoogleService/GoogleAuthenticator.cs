using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Net;
using System.Text;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace MyLearn.GoogleService
{
    public class GoogleAuthenticator
    {
        private AuthResponse _Authenticator;

        public GoogleAuthenticator() { }

        public string GetRefreshToken(string codeFromUser, string clientId, string clientSecret, string redirectURI) {
            this._Authenticator = AuthResponse.Exchange(codeFromUser, clientId, clientSecret, redirectURI);
            if(this._Authenticator == null) {
                return "FAIL";
            } else {
                return this._Authenticator.refresh_token;
            }
        }
    }
}
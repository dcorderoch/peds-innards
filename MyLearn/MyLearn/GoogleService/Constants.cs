using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.GoogleService
{
    // Class that contains all constants used in the Google Service Module
    public static class Constants
    {
        public const string _MyLearnClientId = "530714208118-8n2relpdv57rtt08c359hbiq3ej4jt1r.apps.googleusercontent.com";
        public const string _MyLearnClientSecret = "_8R6dhhuwXRdDJ3kxghnuct1";
        public const string _MyLearnRedirectURI = "urn:ietf:wg:oauth:2.0:oob";
        public const string _MyLearnScopes = "https://www.googleapis.com/auth/drive.file";
        public const string _MyLearnApplicationName = "MyLearn";
        public const string _MyLearnAuthURL = "https://accounts.google.com/o/oauth2/auth?client_id=530714208118-8n2relpdv57rtt08c359hbiq3ej4jt1r.apps.googleusercontent.com&redirect_uri=urn:ietf:wg:oauth:2.0:oob&scope=https://www.googleapis.com/auth/drive.file&response_type=code";
        public const string _GoogleTokenExchangeURL = "https://accounts.google.com/o/oauth2/token";
    }
}
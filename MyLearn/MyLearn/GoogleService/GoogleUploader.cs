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

namespace MyLearn.GoogleService
{
    public class GoogleUploader
    {
        private string[] _GoogleDriveScopes = { DriveService.Scope.DriveFile };
        private GoogleAuthorizationCodeFlow _Flow;
        private TokenResponse _TokenResponse;
        private UserCredential _Credential;
        private DriveService _GDriveService;
        private string ApplicationName = "MyLearn";

        public GoogleUploader(string clientId, string clientSecret, string accessToken, string refreshToken)
        {
            this._Flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = _GoogleDriveScopes,
                DataStore = new FileDataStore("Store")
            });
            this._TokenResponse = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            this._Credential = new UserCredential(this._Flow, Environment.UserName, this._TokenResponse);
            this._GDriveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this._Credential,
                ApplicationName = ApplicationName
            });
        }

        public bool Upload(Stream File)
        {
            bool retVal = true;

            Google.Apis.Drive.v3.Data.File NewFile = null;
            Google.Apis.Drive.v3.Data.File fbody = new Google.Apis.Drive.v3.Data.File();
            try
            {
                var request = this._GDriveService.Files.Create(fbody, File, "");
                request.Upload();
                NewFile = request.ResponseBody;
                Console.WriteLine("File id: " + NewFile.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred in the file part!!!!: " + e.Message);
            }
            return retVal;
        }
    }
}
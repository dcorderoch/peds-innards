using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace MyLearn.GoogleService
{
    // Class that uploads user files to Google Drive.
    public class GoogleUploader
    {
        private string[] _GoogleDriveScopes = { DriveService.Scope.DriveFile };
        private GoogleAuthorizationCodeFlow _Flow;
        private TokenResponse _TokenResponse;
        private UserCredential _Credential;
        private DriveService _GDriveService;
        private string ApplicationName = "MyLearn";

        public GoogleUploader(string refreshToken)
        {
            this._Flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = Constants._MyLearnClientId,
                    ClientSecret = Constants._MyLearnClientSecret
                },
                Scopes = _GoogleDriveScopes,
                DataStore = new FileDataStore("Store")
            });
            this._TokenResponse = new TokenResponse
            {
                AccessToken = "",
                RefreshToken = refreshToken
            };
            this._Credential = new UserCredential(this._Flow, Environment.UserName, this._TokenResponse);
            this._GDriveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this._Credential,
                ApplicationName = ApplicationName
            });
        }

        public string UploadAndReturnDownloadLink(Stream File, string FileName)
        {
            string retVal = "";
            Google.Apis.Drive.v3.Data.File NewFile = null;
            Google.Apis.Drive.v3.Data.File fbody = new Google.Apis.Drive.v3.Data.File();
            fbody.Name = FileName;
            try
            {
                var request = this._GDriveService.Files.Create(fbody, File, "");
                request.Upload();
                NewFile = request.ResponseBody;
                retVal += "https://drive.google.com/file/d/" + NewFile.Id + "/edit?usp=sharing";
            }
            catch (Exception e)
            {
                retVal += "FAIL";
            }
            return retVal;
        }
    }
}
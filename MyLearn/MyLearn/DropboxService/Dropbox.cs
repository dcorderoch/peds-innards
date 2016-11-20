using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using MyLearn.DropboxService.Properties;
using Nemiro.OAuth;
using Nemiro.OAuth.LoginForms;

namespace MyLearn.DropboxService
{
    public partial class Dropbox
    {
        private HttpAuthorization Authorization;

        private string CurrentPath = "";
        private FileStream DownloadFileStream;
        private readonly byte[] DownloadReadBuffer = new byte[4096];

        private Stream DownloadReader;
        private BinaryWriter DownloadWriter;

        private long UploadingFileLength;


        public void DropboxManager()
        {
            if (string.IsNullOrEmpty(Settings.Default.AccessToken))
            {
                GetAccessToken();
            }
            else
            {
                // create authorization header
                Authorization = new HttpAuthorization(AuthorizationType.Bearer, Settings.Default.AccessToken);
            }
        }

        private void GetAccessToken()
        {
            var login = new DropboxLogin("l19qdye91l011oi", "85svbqyvg9tljl9");
            login.Owner = this;
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                Settings.Default.AccessToken = login.AccessToken.Value;
                Settings.Default.Save();

                Authorization = new HttpAuthorization(AuthorizationType.Bearer, login.AccessTokenValue);
            }
            else
            {
                MessageBox.Show("error...");
            }
        }

        private void CreateFolder(string folderName)
        {
            OAuthUtility.PostAsync
                (
                    "https://api.dropboxapi.com/2/files/create_folder",
                    new HttpParameterCollection
                    {
                        new
                        {
                            path =
                                (string.IsNullOrEmpty(CurrentPath) ? "/" : "") +
                                Path.Combine(CurrentPath, folderName).Replace("\\", "/")
                        }
                    },
                    contentType: "application/json",
                    authorization: Authorization,
                    callback: CreateFolder_Result
                );
        }

        private void CreateFolder_Result(RequestResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RequestResult>(CreateFolder_Result), result);
                return;
            }

            if (result.StatusCode == 200)
            {
                GetFiles();
            }
            else
            {
                if (result["error"].HasValue)
                {
                    MessageBox.Show(result["error"].ToString());
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }
            }
        }

        
        private void UploadFile()
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // send file
            var fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            var fileInfo = UniValue.Empty;
            fileInfo["path"] = (string.IsNullOrEmpty(CurrentPath) ? "/" : "") +
                               Path.Combine(CurrentPath, Path.GetFileName(openFileDialog1.FileName)).Replace("\\", "/");
            fileInfo["mode"] = "add";
            fileInfo["autorename"] = true;
            fileInfo["mute"] = false;

            OAuthUtility.PostAsync
                (
                    "https://content.dropboxapi.com/2/files/upload",
                    new HttpParameterCollection
                    {
                        fs
// content of the file
                    },
                    headers: new NameValueCollection {{"Dropbox-API-Arg", fileInfo.ToString()}},
                    contentType: "application/octet-stream",
                    authorization: Authorization,
                    // handler of result
                    callback: Upload_Result,
                    
                );
        }

        
        private void Upload_Result(RequestResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RequestResult>(Upload_Result), result);
                return;
            }

            if (result.StatusCode == 200)
            {
                Get_Shared_Link(result);
           
            }
            else
            {
                if (result["error_summary"].HasValue)
                {
                    MessageBox.Show(result["error_summary"].ToString(), "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(result.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Get_Shared_Link(RequestResult result)
        {
            OAuthUtility.PostAsync
                (
                    "https://api.dropboxapi.com/2/sharing/create_shared_link_with_settings",
                    new HttpParameterCollection
                    {
                        new
                        {
                            path =
                                (string.IsNullOrEmpty(CurrentPath) ? "/" : "") +
                                Path.Combine(CurrentPath, result["path_display"].ToString()).Replace("\\", "/"),
                            settings = new {requested_visibility = "public"}
                        }
                    },
                    contentType: "application/json",
                    authorization: Authorization,
                    // handler of result
                    callback: Share_Result
                );
        }

        private void Share_Result(RequestResult result)
        {
            var publicLink = result["url"].ToString();
            Console.WriteLine(publicLink);
        }
    }
}
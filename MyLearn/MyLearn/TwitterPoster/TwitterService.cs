namespace MyLearn.TwitterPoster
{
    // Class that handles API calls to Twitter
    public class TwitterService
    {
        private readonly OAuthInfo oauth;
        public TwitterService(OAuthInfo initializedCredentials)
        {
            oauth = initializedCredentials;
        }
        public void UpdateStatus(string message)
        {
            new RequestBuilder(oauth, "POST", Constants._TwitterAPIURI)
                .AddParameter("status", message)
                .Execute();
        }
    }
}
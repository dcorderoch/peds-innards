namespace MyLearn.TwitterPoster
{
    public class TwitterServce
    {
        private readonly OAuthInfo oauth;

        public TwitterServce(OAuthInfo initializedCredentials)
        {
            oauth = initializedCredentials;
        }
        public void UpdateStatus(string message)
        {
            new RequestBuilder(oauth, "POST", "https://api.twitter.com/1.1/statuses/update.json")
                .AddParameter("status", message)
                .Execute();
        }
    }
}
namespace MyLearn.TwitterPoster
{
    // Class/Model that contains Oauth2 authentication information for Twitter Requests
    public class OAuthInfo
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessSecret { get; set; }
    }
}
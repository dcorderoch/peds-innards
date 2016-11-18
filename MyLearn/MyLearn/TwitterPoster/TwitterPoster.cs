using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.TwitterPoster
{
    public class TwitterPoster
    {
        private readonly OAuthInfo oauth;

        public TwitterPoster(OAuthInfo initializedCredentials)
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
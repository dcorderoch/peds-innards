using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.TwitterPoster
{
    // Class that updates MyLearn's Twitter.
    public class Tweeter
    {
        public bool tweet(string tweetText)
        {
            bool retval = true;
            var auth = new OAuthInfo()
            {
                ConsumerKey = Constants._MyLearnConsumerKey,
                ConsumerSecret = Constants._MyLearnConsuerSecret,
                AccessToken = Constants._MyLearnAccessToken,
                AccessSecret = Constants._MyLearnConsuerSecret
            };
            var tposter = new TwitterService(auth);
            try
            {
                tposter.UpdateStatus(tweetText);
                retval = true;
            }
            catch (System.Net.WebException e)
            {
                retval = false;
            }
            return retval;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.TwitterPoster
{
    public class Tweeter
    {
        public bool tweet(string tweetText)
        {
            bool retval = true;
            var auth = new OAuthInfo()
            { // hard coded twitter information of the OfficialMyLearn Twitter
                ConsumerKey = "6mjsdenmJfW30AwA30JCFwG4y",
                ConsumerSecret = "ecvASOVKNuwhVKTEI8j5skzH6bf3ZMgRBiPVuFvQ56DgNxq4XT",
                AccessToken = "779106829108379648-i0Q7zca2NDDYWpTuLjosoSKtQO65oNP",
                AccessSecret = "FCcl9Qg0QqUkwLCAHIz4Ufli5VdolfvIRZlxRIWAEZIkQ"
            };
            var tposter = new TwitterServce(auth);
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
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;

namespace MyLearn.BLL
{
    public class BadgeManager
    {
        public ReturnCode GiveBadge(NewBadge newBadge)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }
        public ReturnCode Brag(BadgeIdentifier badgeId)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }
        public static bool tweet()
        {
            bool retval = true;
            var auth = new OAuthInfo()
            {
                ConsumerKey = "6mjsdenmJfW30AwA30JCFwG4y",
                ConsumerSecret = "ecvASOVKNuwhVKTEI8j5skzH6bf3ZMgRBiPVuFvQ56DgNxq4XT",
                AccessToken = "779106829108379648-i0Q7zca2NDDYWpTuLjosoSKtQO65oNP",
                AccessSecret = "FCcl9Qg0QqUkwLCAHIz4Ufli5VdolfvIRZlxRIWAEZIkQ"
            };
            var tposter = new TwitterPoster(auth);
            try
            {
                tposter.UpdateStatus("prueba " + DateTime.Now.ToString());
                retval = true;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("epic fail");
                retval = false;
            }
            return retval;
        }
    }
}
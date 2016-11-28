using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using System.Collections.Generic;

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
        public List<Models.Badge> GetAll(SharedAreaCredentials credentials)
        {
            var retval = new List<Models.Badge>();

            return retval;
        }
        public ReturnCode Brag(BadgeIdentifier badgeId)
        {
            var retVal = new ReturnCode();
            retVal.ReturnStatus = 0;
            // se hace el tweet ANTES de hacer lo demás, si el método retorna FALSE
            // se retorna que falló la vara
            var tweeter = new Tweeter();
            if(tweeter.tweet("mensaje"))
            {
                retVal.ReturnStatus += 1;
                // marcar la maire como que ya se hizo alarde
            }
            // SUBJECT TO CHANGE
            return retVal;
        }
    }
}
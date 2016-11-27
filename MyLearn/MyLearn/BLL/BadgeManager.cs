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
            // se hace el tweet ANTES de hacer lo demás, si el método retorna FALSE
            // se retorna que falló la vara
            var tweeter = new Tweeter();
            if(!tweeter.tweet("mensaje"))
            {
                retVal.ReturnStatus = 0;
            }
            // SUBJECT TO CHANGE
            return retVal;
        }
    }
}
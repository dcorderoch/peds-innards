using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.BLL;

namespace MyLearn.Controllers
{
    public class BadgeController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Give(NewBadge newBadge)
        {
            var status = new BadgeManager();
            var retVal = status.GiveBadge(newBadge);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Brag(BadgeIdentifier badgeId)
        {
            var status = new BadgeManager();
            var retVal = status.Brag(badgeId);
            return Json(retVal);
        }
    }
}

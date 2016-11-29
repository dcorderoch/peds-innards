using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class BidController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewBid newBadge)
        {
            var status = new BidManager();
            var retVal = status.Create(newBadge);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Update(NewBid newBadge)
        {
            var status = new BidManager();
            var retVal = status.Update(newBadge);
            return Json(retVal);
        }
    }
}

using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class BidController : ApiController
    {
        /// <summary>
        /// API Method to add a bid to a job offer auction
        /// </summary>
        /// <param name="newBadge"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewBid newBadge)
        {
            var status = new BidManager();
            var retVal = status.Create(newBadge);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to update a bid to a job offer auction
        /// </summary>
        /// <param name="newBadge"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Update(NewBid newBadge)
        {
            var status = new BidManager();
            var retVal = status.Update(newBadge);
            return Json(retVal);
        }
    }
}

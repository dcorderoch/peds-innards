using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}

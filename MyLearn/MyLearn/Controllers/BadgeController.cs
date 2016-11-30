﻿using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.BLL;
using System.Collections.Generic;

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
        [HttpPost]
        public JsonResult<List<Models.Badge>> GetAll(SharedAreaCredentials credentials)
        {
            var status = new BadgeManager();
            var retVal = status.GetAll(credentials);
            if(retVal == null)
            {
                retVal = new List<Models.Badge>();
            }
            return Json(retVal);
        }
    }
}

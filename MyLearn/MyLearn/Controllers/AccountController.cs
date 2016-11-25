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
    public class AccountController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Disable(UserIdentifier userId)
        {
            var status = new AccountManager();
            var retVal = status.DisableAccount(userId.UserId);
            return Json(retVal);
        }
    }
}

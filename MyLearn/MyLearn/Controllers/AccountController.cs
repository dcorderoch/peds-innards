using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.BLL;

namespace MyLearn.Controllers
{
    public class AccountController : ApiController
    {
        /// <summary>
        /// API Method to disable or enable a user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> ToggleAccount(UserIdentifier userId)
        {
            var status = new AccountManager();
            var retVal = status.ToggleAccount(userId.UserId);
            return Json(retVal);
        }
    }
}

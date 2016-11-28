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
            var retVal = status.ToggleAccount(userId.UserId);
            return Json(retVal);
        }
    }
}

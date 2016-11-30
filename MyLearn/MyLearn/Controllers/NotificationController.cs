using MyLearn.BLL;
using MyLearn.InputModels;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyLearn.Controllers
{
    public class NotificationController : ApiController
    {
        [HttpPost]
        public JsonResult<List<string>> GetByStudent(StudentIdentifier studentID)
        {
            var notifMngr = new NotificationManager();
            var retVal = notifMngr.GetStudentNotifications(studentID.StudentUserId);
            if (retVal == null)
            {
                retVal = new List<string>();
            }
            return Json(retVal);
        }
    }
}

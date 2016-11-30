using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class StudentController : ApiController
    {
        [HttpPost]
        public JsonResult<StudentProfileAsEmployer> GetProfile(StudentIdentifier studentId)
        {
            var studentMngr = new StudentManager();
            var retVal = studentMngr.GetProfile(studentId);
            if (retVal == null)
            {
                retVal = new StudentProfileAsEmployer();
            }
            return Json(retVal);
        }
    }
}

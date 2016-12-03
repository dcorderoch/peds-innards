using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class ProjectController : ApiController
    {
        /// <summary>
        /// API Method to register a new student to de the MyLearn Database
        /// </summary>
        /// <param name="proposal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Propose(ProjectProposal proposal)
        {
            var courseMngr = new CourseManager();
            var retVal = courseMngr.Propose(proposal);
            return Json(retVal);
        }
    }
}

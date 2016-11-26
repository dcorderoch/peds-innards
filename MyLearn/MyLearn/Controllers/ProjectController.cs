using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class ProjectController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Propose(ProjectProposal proposal)
        {
            var courseMngr = new CourseManager();
            var retVal = courseMngr.Propose(proposal);
            return Json(retVal);
        }
    }
}

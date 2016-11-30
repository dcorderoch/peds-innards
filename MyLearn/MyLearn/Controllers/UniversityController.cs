using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class UniversityController : ApiController
    {
        [HttpGet]
        public JsonResult<List<University>> GetAll()
        {
            var uniMngr = new UniversityManager();
            var retVal = uniMngr.GetAllUniversities();
            if (retVal == null)
            {
                retVal = new List<University>();
            }
            return Json(retVal);
        }

    }
}

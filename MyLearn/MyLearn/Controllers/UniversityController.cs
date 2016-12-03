using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class UniversityController : ApiController
    {
        /// <summary>
        /// API Method that returns all universities in the MyLearn Database
        /// </summary>
        /// <returns></returns>
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

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class TechnologyController : ApiController
    {
        /// <summary>
        /// API Method that returns all Technologies in the MyLearn Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<Technology>> GetAll()
        {
            var techMngr = new TechnologyManager();
            var retVal = techMngr.GetAllTechnologies();
            if (retVal == null)
            {
                retVal = new List<Technology>();
            }
            return Json(retVal);
        }
        /// <summary>
        /// API Method to Add a new Technology to the MyLearn Database
        /// </summary>
        /// <param name="newTech"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewTechnology newTech)
        {
            var techMngr = new TechnologyManager();
            var retVal = techMngr.CreateTechnology(newTech.TechnologyName);
            return Json(retVal);
        }
    }
}

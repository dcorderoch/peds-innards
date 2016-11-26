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
    public class TechnologyController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Technology>> GetAll()
        {
            var techMngr = new TechnologyManager();
            var retVal = techMngr.GetAllTechnologies();
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewTechnology newTech)
        {
            var techMngr = new TechnologyManager();
            var retVal = techMngr.CreateTechnology(newTech.TechnologyName);
            return Json(retVal);
        }
    }
}

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
    public class UniversityController : ApiController
    {
        [HttpGet]
        public JsonResult<List<University>> GetAll()
        {
            var uniMngr = new UniversityManager();
            var retVal = uniMngr.GetAllUniversities();
            return Json(retVal);
        }

    }
}

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
    public class CountryController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Country>> GetAll()
        {
            var cntrMngr = new CountryManager();
            var retVal = cntrMngr.GetAllCountries();
            return Json(retVal);
        }
    }
}

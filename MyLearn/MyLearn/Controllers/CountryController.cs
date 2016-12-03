using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class CountryController : ApiController
    {
        /// <summary>
        /// API Method to get all countries in the MyLearn Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<Country>> GetAll()
        {
            var cntrMngr = new CountryManager();
            var retVal = cntrMngr.GetAllCountries();
            if(retVal == null)
            {
                retVal = new List<Country>();
            }
            return Json(retVal);
        }
    }
}

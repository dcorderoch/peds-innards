using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class LanguageController : ApiController
    {
        /// <summary>
        /// API Method to return all languages in the MyLearn Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<Language>> GetAll()
        {
            var langMngr = new LanguageManager();
            var retVal = langMngr.GetAllLanguages();
            if (retVal == null)
            {
                retVal = new List<Language>();
            }
            return Json(retVal);
        }
    }
}

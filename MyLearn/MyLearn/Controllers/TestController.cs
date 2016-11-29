using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyLearn.Controllers
{
    public class TestController : ApiController
    {
        /// <summary>
        /// This Method returns all information of all Products in the Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<List<string>> GetAll()
        {
            List<string> retVal = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                var crap = new string(string.Format("haha{0}", i.ToString()).ToCharArray());
                retVal.Add(crap);
            }
            return Json(retVal);
        }
        [HttpGet]
        public JsonResult<List<string>> GetTHEM()
        {
            List<string> retVal = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                var crap = new string(string.Format("PORN... OGRAPHY!{0}", i.ToString()).ToCharArray());
                retVal.Add(crap);
            }
            return Json(retVal);
        }
    }
}

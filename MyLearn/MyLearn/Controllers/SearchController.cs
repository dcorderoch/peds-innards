using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.InputModels;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// API Method that returns a number of top students in a Search call to the MyLearn REST API, according to a pre-defined formula provided by X-MP
        /// </summary>
        /// <param name="numberOfStudents"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<List<TopStudent>> GetTop(NumberOfTopStudents numberOfStudents)
        {
            var searchMngr = new SearchManager();
            var retVal = searchMngr.GetTopStudents(numberOfStudents.NumberOfStudents);
            if (retVal == null)
            {
                retVal = new List<TopStudent>();
            }
            return Json(retVal);
        }
        /// <summary>
        /// API Method that returns a number of top students in a Search call to the MyLearn REST API, with custom criteria for the selection of top students
        /// </summary>
        /// <param name="customStudents"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<List<TopStudent>> GetTopSelective(CustomTopStudent customStudents)
        {
            var searchMngr = new SearchManager();
            var retVal = searchMngr.
                GetTopStudentsByCriteria(customStudents.NumberOfTopStudents,
                customStudents.CourseAvgWeight, customStudents.CourseSuccessRateWeight,
                customStudents.ProjectAvgWeight, customStudents.ProjectSuccessRateWeight);
            if (retVal == null)
            {
                retVal = new List<TopStudent>();
            }
            return Json(retVal);
        }
    }
}

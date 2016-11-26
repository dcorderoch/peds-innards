using MyLearn.BLL;
using MyLearn.InputModels;
using MyLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyLearn.Controllers
{
    public class SearchController : ApiController
    {
        [HttpPost]
        public JsonResult<List<TopStudent>> GetTop(NumberOfTopStudents numberOfStudents)
        {
            var searchMngr = new SearchManager();
            var retVal = searchMngr.GetTopStudents(numberOfStudents.NumberOfStudents);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<TopStudent>> GetTopSelective(CustomTopStudent customStudents)
        {
            var searchMngr = new SearchManager();
            var retVal = searchMngr.
                GetTopStudentsByCriteria(customStudents.NumberOfTopStudents,
                customStudents.CourseAvgWeight, customStudents.CourseSuccessRateWeight,
                customStudents.ProjectAvgWeight, customStudents.ProjectSuccessRateWeight);
            return Json(retVal);
        }
    }
}

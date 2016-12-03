﻿using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class StudentController : ApiController
    {
        /// <summary>
        /// API Method that returns all information in the profile of a Student, used by an Employer
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<StudentProfileAsEmployer> GetProfile(StudentIdentifier studentId)
        {
            var studentMngr = new StudentManager();
            var retVal = studentMngr.GetProfile(studentId);
            if (retVal == null)
            {
                retVal = new StudentProfileAsEmployer();
            }
            return Json(retVal);
        }
    }
}

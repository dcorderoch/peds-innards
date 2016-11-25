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
    public class CourseController : ApiController
    {
        [HttpPost]
        public JsonResult<Course> GetCourseAsProfessor(CourseIdentifier courseId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetCourseAsProfessor(courseId.CourseId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<SpecificCourse> GetSpecificCourse(SharedAreaCredentials credentials)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetSpecificCourse(credentials);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<CourseAsStudent> GetCourseAsStudent(CourseIdentifier courseId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetCourseAsStudent(courseId.CourseId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewCourse newCourse)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.CreateCourse(newCourse);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<AllProfessorsCourses> GetAllByProfessor(ProfessorIdentifier profCredentials)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetAllByProfessor(profCredentials.ProfUserId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<CourseShort>> GetAllByUniversity(UniversityIdentifier universityId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetAllByUniversity(universityId.UniversityId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Close(UniversityIdentifier universityId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.CloseCourse(universityId.UniversityId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Join(StudentJoinsCourse joiningStudentInfo)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.Join(joiningStudentInfo);
            return Json(retVal);
        }
    }
}

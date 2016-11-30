using System.Collections.Generic;
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
            if (retVal == null)
            {
                retVal = new Course();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<SpecificCourse> GetSpecificCourse(SharedAreaCredentials credentials)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetSpecificCourse(credentials);
            if (retVal == null)
            {
                retVal = new SpecificCourse();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<CourseAsStudent> GetCourseAsStudent(CourseAsStudentCredentials courseId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetCourseAsStudent(courseId);
            if (retVal == null)
            {
                retVal = new CourseAsStudent();
            }
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
            if (retVal == null)
            {
                retVal = new AllProfessorsCourses();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<CourseShort>> GetAllByUniversity(UniversityIdentifier universityId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetAllByUniversity(universityId.UniversityId);
            if (retVal == null)
            {
                retVal = new List<CourseShort>();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Close(CourseIdentifier universityId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.CloseCourse(universityId.CourseId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Join(StudentJoinsCourse joiningStudentInfo)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.Join(joiningStudentInfo);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<StudentCourses> GetAllByStudent(StudentIdentifier studentCredentials)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetAllByStudent(studentCredentials.StudentUserId);
            if (retVal == null)
            {
                retVal = new StudentCourses();
            }
            return Json(retVal);
        }
    }
}

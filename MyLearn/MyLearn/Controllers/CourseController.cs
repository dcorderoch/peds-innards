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
        /// <summary>
        /// API Method to get all information of a course as a Professor
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all information of a course project (by a student) as a Professor, sans comments
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all information of a course as a Student, sans comments
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to add a new course to the MyLearn Database as a Professor
        /// </summary>
        /// <param name="newCourse"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewCourse newCourse)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.CreateCourse(newCourse);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to get all courses taught by a Professor, as a Professor
        /// </summary>
        /// <param name="profCredentials"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all courses taught at a particular University
        /// </summary>
        /// <param name="universityId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// /// API Method to close a course taught by a Professor, and finalize all its student projects
        /// </summary>
        /// <param name="universityId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Close(CourseIdentifier universityId)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.CloseCourse(universityId.CourseId);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to Join a Course as a Student
        /// </summary>
        /// <param name="joiningStudentInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Join(StudentJoinsCourse joiningStudentInfo)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.Join(joiningStudentInfo);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to get all courses of a particular student, both active and finished
        /// </summary>
        /// <param name="studentCredentials"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get a student's partial grade of a course
        /// </summary>
        /// <param name="studentCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<PartialGrade> GetGrade(StudentAndCourse stAndCourse)
        {
            var courMngr = new CourseManager();
            var retVal = courMngr.GetStudentGrade(stAndCourse);
            if (retVal == null)
            {
                retVal = new StudentCourses();
            }
            return Json(retVal);
        }
    }
}

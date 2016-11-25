using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.InputModels;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class CourseManager
    {
        public ReturnCode CreateCourse(NewCourse newCourse)
        {
            ReturnCode retVal = new ReturnCode();

            //Add new course to DB
            /*dbobject.Add(newCourse.CourseName);
            dbobject.Add(newCourse.CourseDescription);
            dbobject.Add(newCourse.ProfUserId);
            dbobject.Add(newCourse.UniversityId);
            dbobject.Add(newCourse.Group);
            dbobject.Add(newCourse.MinGrade);
            dbobject.Add(newCourse.Badges);*/
            //hard-coded
            retVal.StatusCode = 1;
            return retVal;
        }

        public ReturnCode CloseCourse(string courseId)
        {
            ReturnCode returnCode = new ReturnCode();
            //Call function
            int retVal = 0;
            //retVal  = CloseCourseDB(courseId);
            returnCode.StatusCode = retVal;
            return returnCode;
        }

        public Course GetCourseAsProfessor(string courseId)
        {
            Course course = new Course();
            //Subject to change
            course.CourseName = "";
            course.UniversityId = "";
            course.MinGrade = 0;
            course.CourseId = "";
            course.CourseDescription = "";
            course.Group = 2;
            course.Students = new List<StudentInCourse>();
            return course;

        }

        public GetSpecificCourse(string studentId, string professorId, string universityId,
            string group, string courseId)
        {
            
        }

        public GetCourseAsStudent(string courseId)
        {
            
        }

        public GetAllByProfessor(string professorId)
        {
            
        }

        public GetAllByUniversity(string universityId)
        {
            
        }

    }
}
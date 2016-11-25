using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.InputModels;
using MyLearn.Models;
using Badge = MyLearn.Models.Badge;

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
            retVal.ReturnStatus = 1;
            return retVal;
        }

        public ReturnCode CloseCourse(string courseId)
        {
            ReturnCode returnCode = new ReturnCode();
            //Call function
            int retVal = 0;
            //retVal  = CloseCourseDB(courseId);
            returnCode.ReturnStatus = retVal;
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

        public SpecificCourse GetSpecificCourse(SharedAreaCredentials credentials)
        {
         SpecificCourse specificCourse = new SpecificCourse();
            specificCourse.NombreContacto = "";
            specificCourse.ApellidoContacto = "";
            specificCourse.Grade = 0;
            specificCourse.Badges = new List<Badge>();

            return specificCourse;   
        }

        public CourseAsStudent GetCourseAsStudent(string courseId)
        {
        CourseAsStudent courseAsStudent = new CourseAsStudent();
            //Get course from database
            courseAsStudent.CourseName = "";
            courseAsStudent.StudentUserId = "";
            courseAsStudent.ProfUserId = "";
            courseAsStudent.ProfessorName = "";
            courseAsStudent.UniversityId = "";
            courseAsStudent.Grade = 10;
            courseAsStudent.Badges = new List<Badge>();
            courseAsStudent.CourseId = "";
            courseAsStudent.CourseDescription = "";
            courseAsStudent.Group = 1;
            courseAsStudent.CourseState = 1;
            return courseAsStudent;
                
        }

        public AllProfessorsCourses GetAllByProfessor(string professorId)
        {
            AllProfessorsCourses allCourses = new AllProfessorsCourses();
            //get all professor's courses
            List<ActiveCourse> activeCourses = new List<ActiveCourse>();
            List<FinishedCourse> finishedCourses = new List<FinishedCourse>();
            //get from db and add them here, once the repositories are up and running

            //activeCourses = ;
            //finishedCourses = ;

            allCourses.ActiveCourses = activeCourses;
            allCourses.FInishedCourses = finishedCourses;

            return allCourses;
        }

        public List<CourseShort> GetAllByUniversity(string universityId)
        {
         List<CourseShort> listOfCourses = new List<CourseShort>();
            //get courses by universityId
            //listOfCourses= dbobject(universityId);
            return listOfCourses;
        }

        public ReturnCode Join(StudentJoinsCourse joiningStudent)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }

    }
}
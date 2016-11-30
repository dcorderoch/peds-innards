using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.Models;

namespace MyLearn.Utils
{
    public class ModelMapper
    {
        public List<ActiveCourse> ActiveCourseListMap(List<MyLearnDAL.Models.Course> activeCourses)
        {
            List<ActiveCourse> resultList = new List<ActiveCourse>();
            foreach (var course in activeCourses)
            {
                var resCourse = new ActiveCourse();
                resCourse.CourseId = course.CourseId.ToString();
                resCourse.CourseDescription = course.Description;
                resCourse.CourseName = course.Name;
                resCourse.Accepted = course.IsActive;
                resultList.Add(resCourse);
            }

            return resultList;
        }

        public List<FinishedCourse> FinishedCourseListMap(List<MyLearnDAL.Models.Course> finishedCourses)
        {
            List<FinishedCourse> resultList = new List<FinishedCourse>();
            foreach (var course in finishedCourses)
            {
                var resCourse = new FinishedCourse();
                resCourse.CourseId = course.CourseId.ToString();
                resCourse.CourseDescription = course.Description;
                resCourse.CourseName = course.Name;
                resultList.Add(resCourse);
            }
            return resultList;
        }

        public List<CourseShort> CourseShortListMap(List<MyLearnDAL.Models.Course> courses)
        {
            List<CourseShort> resultList = new List<CourseShort>();
            foreach (var course in courses)
            {
                var resCourse = new CourseShort();
                resCourse.CourseId = course.CourseId.ToString();
                resCourse.CourseDescription = course.Description;
                resCourse.CourseName = course.Name;
                resultList.Add(resCourse);
            }
            return resultList;
        }
    }
}
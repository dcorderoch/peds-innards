using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using Badge = MyLearn.Models.Badge;
using JobOffer = MyLearnDAL.Models.JobOffer;

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
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    var resCourse = new CourseShort();
                    resCourse.CourseId = course.CourseId.ToString();
                    resCourse.CourseDescription = course.Description;
                    resCourse.CourseName = course.Name;
                    resultList.Add(resCourse);
                }
            }
            return resultList;
        }

        public List<Badge> BadgeListMap(List<MyLearnDAL.Models.Badge> listOfBadges)
        {
            List<Badge> resultList = new List<Badge>();
            foreach (var newBadge in listOfBadges)
            {
                var badge = new Badge();
                badge.BadgeId = newBadge.AchievementId.ToString();
                badge.BadgeDescription = newBadge.Achievement.Description;
                badge.Alardeado = newBadge.Bragged;
                badge.Awarded = 1;
                badge.Value = newBadge.Achievement.Score;
                resultList.Add(badge);
            }
            return resultList;
        }

        public List<Achievement> BadgeToAchievementListMap(List<Badge> badges, MyLearnDAL.Models.Course course)
        {
            var achievements = new List<Achievement>();
            foreach (var badge in badges)
            {
                var achievement = new Achievement();
                achievement.AchievementId = new Guid(badge.BadgeId);
                achievement.CourseId = course.CourseId;
                achievement.Description = badge.BadgeDescription;
                achievement.Score = badge.Value;
                achievements.Add(achievement);
            }
            return achievements;
        }

        public List<StudentInCourse> StudentInCourseMap(List<Student> listOfStudents)
        {
            List<StudentInCourse> resultList = new List<StudentInCourse>();
            foreach (var student in listOfStudents)
            {
                var resStudent = new StudentInCourse();
                resStudent.StudentUserId = student.UserId.ToString();
                resStudent.Nombre = student.Name;
                resultList.Add(resStudent);
            }
            return resultList;
        }
        
 



}
}
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
            if (!activeCourses.Any()) return resultList;
            foreach (var course in activeCourses)
            {
                var resCourse = new ActiveCourse
                {
                    CourseId = course.CourseId.ToString(),
                    CourseDescription = course.Description,
                    CourseName = course.Name,
                    Accepted = course.IsActive
                };
                resultList.Add(resCourse);
            }

            return resultList;
        }

        public List<FinishedCourse> FinishedCourseListMap(List<MyLearnDAL.Models.Course> finishedCourses)
        {
            List<FinishedCourse> resultList = new List<FinishedCourse>();
            if (!finishedCourses.Any()) return resultList;
            foreach (var course in finishedCourses)
            {
                var resCourse = new FinishedCourse
                {
                    CourseId = course.CourseId.ToString(),
                    CourseDescription = course.Description,
                    CourseName = course.Name
                };
                resultList.Add(resCourse);
            }
            return resultList;
        }

        public List<ActiveJobOffer> ActiveJobListMap(List<MyLearnDAL.Models.JobOffer> activeJobOffers)
        {
            List<ActiveJobOffer> resultList = new List<ActiveJobOffer>();
            if (!activeJobOffers.Any()) return resultList;
            foreach (var jobOffer in activeJobOffers)
            {
                var resJob = new ActiveJobOffer
                {
                    JobOffer = jobOffer.Name,
                    JobOfferId = jobOffer.JobOfferId.ToString(),
                    EmployerName = jobOffer.Employer.CompanyName,
                    Description = jobOffer.Description
                };
                resultList.Add(resJob);
            }
            return resultList;
        }

        public List<FinishedJobOffer> FinishedJobListMap(List<MyLearnDAL.Models.JobOffer> finishedJobOffers)
        {
            List<FinishedJobOffer> resultList = new List<FinishedJobOffer>();
            if (!finishedJobOffers.Any()) return resultList;
            foreach (var jobOffer in finishedJobOffers)
            {
                var resJob = new FinishedJobOffer
                {
                    JobOffer = jobOffer.Name,
                    JobOfferId = jobOffer.JobOfferId.ToString(),
                    EmployerName = jobOffer.Employer.CompanyName,
                    Description = jobOffer.Description
                };
                resultList.Add(resJob);
            }
            return resultList;
        }

        public List<CourseShort> CourseShortListMap(List<MyLearnDAL.Models.Course> courses)
        {
            List<CourseShort> resultList = new List<CourseShort>();
            if (!courses.Any()) return resultList;
            foreach (var course in courses)
            {
                var resCourse = new CourseShort
                {
                    CourseId = course.CourseId.ToString(),
                    CourseDescription = course.Description,
                    CourseName = course.Name,
                    Active = course.IsActive
                };
                resultList.Add(resCourse);
            }
            return resultList;
        }

        public List<Badge> BadgeListMap(List<MyLearnDAL.Models.Badge> listOfBadges)
        {
            List<Badge> resultList = new List<Badge>();
            if (!listOfBadges.Any()) return resultList;
            foreach (var newBadge in listOfBadges)
            {
                var badge = new Badge
                {
                    BadgeId = newBadge.AchievementId.ToString(),
                    BadgeDescription = newBadge.Achievement.Description,
                    Alardeado = newBadge.Bragged,
                    Awarded = 1,
                    Value = newBadge.Achievement.Score
                };
                resultList.Add(badge);
            }
            return resultList;
        }

        public List<Achievement> BadgeToAchievementListMap(List<Badge> badges, MyLearnDAL.Models.Course course)
        {
            var achievements = new List<Achievement>();
            if (!badges.Any()) return achievements;
            foreach (var badge in badges)
            {
                var achievement = new Achievement
                {
                    AchievementId = Guid.NewGuid(),
                    CourseId = course.CourseId,
                    Description = badge.BadgeDescription,
                    Score = badge.Value
                };
                achievements.Add(achievement);
            }
            return achievements;
        }

        public List<StudentInCourse> StudentInCourseMap(List<Student> listOfStudents)
        {
            List<StudentInCourse> resultList = new List<StudentInCourse>();
            if (!listOfStudents.Any()) return resultList;
            foreach (var student in listOfStudents)
            {
                var resStudent = new StudentInCourse
                {
                    StudentUserId = student.UserId.ToString(),
                    Nombre = student.Name
                };
                resultList.Add(resStudent);
            }
            return resultList;
        }

        public List<string> TechnologiesToString(List<MyLearnDAL.Models.Technology> technologies)
        {
            var resultList = new List<string>();
            if (!technologies.Any()) return resultList;
            foreach (var tech in technologies)
            {
                var strTech = tech.Name;
                resultList.Add(strTech);
            }
            return resultList;
        }

        public List<JobOfferBid> JobOfferBidMap(List<MyLearnDAL.Models.Bid> bidList)
        {
            List<JobOfferBid> result = new List<JobOfferBid>();
            if (bidList.Any())
            {
                foreach (var bid in bidList)
                {
                    JobOfferBid resBid = new JobOfferBid();
                    resBid.Money = bid.Money;
                    resBid.DurationInDays = bid.Duration;
                    resBid.StudentName = bid.Student.Name;
                    resBid.StudentSurname = bid.Student.LastName;
                    resBid.StudentUserId = bid.Student.UserId.ToString();
                    result.Add(resBid);
                }
            }
            return result;
        }
    }
}

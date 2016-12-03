using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.BLL;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using Badge = MyLearn.Models.Badge;
using JobOffer = MyLearnDAL.Models.JobOffer;

namespace MyLearn.Utils
{
    /// <summary>
    /// Class built in order to map Models in the API to the DAL Models or vice versa. 
    /// </summary>
    public class ModelMapper
    {
        /// <summary>
        /// Maps a list of dal's active courses to api's active courses.
        /// </summary>
        /// <param name="activeCourses"></param>
        /// <returns></returns>
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
                    Accepted = 0
                };
                resultList.Add(resCourse);
            }

            return resultList;
        }
        /// <summary>
        /// Maps dal's finished courses to api's finished courses.
        /// </summary>
        /// <param name="finishedCourses"></param>
        /// <returns></returns>
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
                    CourseName = course.Name,
                    Accepted = 0
                };
                resultList.Add(resCourse);
            }
            return resultList;
        }

        /// <summary>
        /// Maps active job offers from dal's to api's model.
        /// </summary>
        /// <param name="activeJobOffers"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Maps finished jobs from dal's to api's model.
        /// </summary>
        /// <param name="finishedJobOffers"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Maps dal's courses to course short model in api.
        /// </summary>
        /// <param name="courses"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Maps badges from dal's to api's model.
        /// </summary>
        /// <param name="listOfBadges"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Maps a badge to achievement.
        /// </summary>
        /// <param name="badges"></param>
        /// <param name="course"></param>
        /// <returns>List of achievements.</returns>
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

        /// <summary>
        /// Maps a list of students to a list of students in course.
        /// </summary>
        /// <param name="listOfStudents"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Maps technology object to string.
        /// </summary>
        /// <param name="technologies"></param>
        /// <returns></returns>
        public List<string> TechnologiesToString(List<MyLearnDAL.Models.Technology> technologies)
        {
            var resultList = new List<string>();
            if (!technologies.Any()) return resultList;
            foreach (var tech in technologies)
            {
                resultList.Add(tech.Name);
            }
            return resultList;
        }

        /// <summary>
        /// Maps language object to string.
        /// </summary>
        /// <param name="languages"></param>
        /// <returns></returns>
        public List<string> LanguagesToString(List<MyLearnDAL.Models.Language> languages)
        {
            var resultList = new List<string>();
            if (!languages.Any()) return resultList;
            foreach (var lang in languages)
            {
                resultList.Add(lang.Name);
            }
            return resultList;
        }

        /// <summary>
        ///Maps bid list to job offer bid list.
        /// </summary>
        /// <param name="bidList"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Maps a job offer from dal's model to api's.
        /// </summary>
        /// <param name="jobOffers"></param>
        /// <returns></returns>
        public List<Models.JobOffer> JobOfferMap(List<JobOffer> jobOffers)
        {
            var result = new List<Models.JobOffer>();
            var modelMapper = new ModelMapper();
            if (!jobOffers.Any()) return result;
            foreach (var jobOffer in jobOffers)
            {
                Models.JobOffer resJobOffer = new Models.JobOffer();
                resJobOffer.Budget = jobOffer.Budget;
                resJobOffer.Description = jobOffer.Description;
                resJobOffer.StartDate = jobOffer.StartDate.ToString();
                resJobOffer.EndDate = jobOffer.EndDate.ToString();
                resJobOffer.Technologies = modelMapper.TechnologiesToString(jobOffer.Technologies);
                resJobOffer.State = jobOffer.IsActive;
                resJobOffer.JobOfferTitle = jobOffer.Name;
                resJobOffer.JobOfferId = jobOffer.JobOfferId.ToString();
                resJobOffer.StateDescription = jobOffer.StateDescription;
                resJobOffer.EmployerUserId = jobOffer.EmployerId.ToString();
                resJobOffer.EmployerName = jobOffer.Employer.CompanyName;
                result.Add(resJobOffer);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class SearchManager
    {
        /// <summary>
        /// Get top list of students for the default criteria
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="numberOfStudents"></param>
        /// <returns></returns>
        public List<TopStudent> GetTopStudentsByCountry(string countryId, int numberOfStudents)
        {
            using (var context = new MyLearnContext())
            {
                var topStudents = new List<TopStudent>();
                if (numberOfStudents <= 0 || numberOfStudents > 1000) return topStudents;
                var studentRepo = new StudentRepository(context);
                var retStudents = studentRepo.getStudentsByCountryId(Guid.Parse(countryId));
                topStudents = retStudents.Select(student => new TopStudent()
                {
                    Name = student.Name,
                    Email = student.Email,
                    PhoneNum = student.PhoneNum,
                    PlaceInSearch = getStudentIndexByCountry((decimal)0.3, (decimal)0.3, (decimal)0.3, (decimal)0.1, student)
                }).OrderByDescending(s => s.PlaceInSearch).Take(numberOfStudents).ToList();
                studentRepo.Dispose();
                return topStudents;
            }
        }
        
        /// <summary>
        /// Get top list of students for the given criteria
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="numberOfStudents"></param>
        /// <param name="courseAvgWeight"></param>
        /// <param name="courseSuccessRateWeight"></param>
        /// <param name="projectAvgWeight"></param>
        /// <param name="projectSuccessRateWeight"></param>
        /// <returns></returns>
        public List<TopStudent> GetCustomTopStudentsByCountry(string countryId, int numberOfStudents, decimal courseAvgWeight,
            decimal courseSuccessRateWeight, decimal projectAvgWeight, decimal projectSuccessRateWeight)
        {
            using (var context = new MyLearnContext())
            {
                var topStudents = new List<TopStudent>();
                decimal weight = courseAvgWeight + courseSuccessRateWeight + projectAvgWeight + projectSuccessRateWeight;
                if (numberOfStudents <= 0 || numberOfStudents > 1000 ||
                    (weight).CompareTo((decimal)1.0)!=0) return topStudents;
                var studentRepo = new StudentRepository(context);
                var retStudents = studentRepo.getStudentsByCountryId(Guid.Parse(countryId));
                topStudents = retStudents.Select(student => new TopStudent()
                {
                    Name = student.Name,
                    Email = student.Email,
                    PhoneNum = student.PhoneNum,
                    PlaceInSearch = getStudentIndexByCountry(projectAvgWeight, courseAvgWeight, projectSuccessRateWeight, courseSuccessRateWeight, student)
                }).OrderByDescending(s => s.PlaceInSearch).Take(numberOfStudents).ToList();
                studentRepo.SaveChanges();
                return topStudents;
            }
        }

        /// <summary>
        /// Calculate the student PlaceInSearch by the given atributes weight
        /// </summary>
        /// <param name="avgProjectW"></param>
        /// <param name="avgCourseW"></param>
        /// <param name="suceedCourseW"></param>
        /// <param name="suceedProjectW"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        private int getStudentIndexByCountry(decimal avgProjectW, decimal avgCourseW, decimal suceedProjectW, decimal suceedCourseW,
             MyLearnDAL.Models.Student student)
        {
            decimal totalProjects = student.NumFailedProjects + student.NumSuceedProjects;
            decimal totalCourses = student.NumFailedCourses + student.NumSuceedCourses;
            decimal totalProjectRate = (decimal)0.0, totalCourseRate = (decimal)0.0;
            if (!totalProjects.Equals((decimal)0.0))
            {
                totalProjectRate = student.NumSuceedProjects/totalProjects;
            }
            if (!totalCourses.Equals((decimal)0.0))
            {
                totalCourseRate = student.NumSuceedCourses/totalCourses;
            }
            var index = (int) (avgCourseW* (decimal)student.AvgCourses + avgProjectW* (decimal)student.AvgProjects + totalCourseRate*suceedCourseW +
                        totalProjectRate*suceedProjectW);
            return index;
        }
    }
}
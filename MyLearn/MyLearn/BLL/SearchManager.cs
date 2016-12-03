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
                var studentRepo = new StudentRepository(context);
                var retStudents = studentRepo.getStudentsByCountryId(Guid.Parse(countryId));
                var topStudents = retStudents.Select(student => new TopStudent() {Name = student.Name, Email = student.Email, PhoneNum = student.PhoneNum,
                    PlaceInSearch = getStudentIndexByCountry(0.3, 0.3, 0.3, 0.1, student)}).OrderBy(s => s.PlaceInSearch).Take(numberOfStudents).ToList();
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
        public List<TopStudent> GetCustomTopStudentsByCountry(string countryId, int numberOfStudents, double courseAvgWeight,
            double courseSuccessRateWeight, double projectAvgWeight, double projectSuccessRateWeight)
        {
            using (var context = new MyLearnContext())
            {
                var studentRepo = new StudentRepository(context);
                var retStudents = studentRepo.getStudentsByCountryId(Guid.Parse(countryId));
                var topStudents = retStudents.Select(student => new TopStudent()
                {
                    Name = student.Name,
                    Email = student.Email,
                    PhoneNum = student.PhoneNum,
                    PlaceInSearch = getStudentIndexByCountry(projectAvgWeight,courseAvgWeight,projectSuccessRateWeight,courseSuccessRateWeight, student)
                }).OrderBy(s => s.PlaceInSearch).Take(numberOfStudents).ToList();
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
        private int getStudentIndexByCountry(double avgProjectW, double avgCourseW, double suceedProjectW, double suceedCourseW,
             MyLearnDAL.Models.Student student)
        {
            double totalProjects = student.NumFailedProjects + student.NumSuceedProjects;
            double totalCourses = student.NumFailedCourses + student.NumSuceedCourses;
            double totalProjectRate = 0.0, totalCourseRate = 0.0;
            if (!totalProjects.Equals(0.0))
            {
                totalProjectRate = student.NumSuceedProjects/totalProjects;
            }
            if (!totalCourses.Equals(0.0))
            {
                totalCourseRate = student.NumSuceedCourses/totalCourses;
            }
            var index = (int) (avgCourseW* (double)student.AvgCourses + avgProjectW* (double)student.AvgProjects + totalCourseRate*suceedCourseW +
                        totalProjectRate*suceedProjectW);
            return index;
        }
    }
}
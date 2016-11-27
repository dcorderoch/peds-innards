using System.Collections.Generic;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class SearchManager
    {
        public List<TopStudent> GetTopStudents(int numberOfStudents)
        {
            List<TopStudent> topStudents = new List<TopStudent>();
            for (int i = 0; i < numberOfStudents; i++)
            {
               TopStudent topStudent = new TopStudent();
                /*topStudent.Name = student.Name;
                topStudent.Email = student.Email;
                topStudent.PhoneNum = student.PhoneNum;*/
                topStudents.Add(topStudent);
            }
            return topStudents;
        }
        
        public List<TopStudent> GetTopStudentsByCriteria(int numberOfStudents, int courseAvgWeight, 
            int courseSuccessRateWeight, int projectAvgWeight, int projectSuccessRateWeight)
        {
            List<TopStudent> topStudents = new List<TopStudent>();
            for (int i = 0; i < numberOfStudents; i++)
            {
                /*if(student.courseSuccessRateWeight >= courseSuccessRateWeight && 
                    student.projectAvgWeight >= projectAvgWeight && 
                    student.projectSuccessRateWeight >= projectSuccessRateWeight) { */
                TopStudent topStudent = new TopStudent();
                    /*topStudent.Name = student.Name;
                    topStudent.Email = student.Email;
                    topStudent.PhoneNum = student.PhoneNum;*/
                    topStudents.Add(topStudent);
                //}
                
            }
            return topStudents;
        }
    }
}
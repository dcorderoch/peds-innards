using System.Collections.Generic;

namespace MyLearn.Models
{
    public class AllProfessorsCourses
    {
        public List<ActiveCourse> ActiveCourses { get; set; }
        public List<FinishedCourse> FinishedCourses { get; set; }
    }
}
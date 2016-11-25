using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class AllProfessorsCourses
    {
        public List<ActiveCourse> ActiveCourses { get; set; }
        public List<FinishedCourse> FInishedCourses { get; set; }
    }
}
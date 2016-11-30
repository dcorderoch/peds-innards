using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class StudentCourses
    {
        public List<FinishedCourse> FinishedCourses { get; set; }
        public List<ActiveCourse> ActiveCourses { get; set; }
    }
}
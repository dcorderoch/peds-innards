using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class Course
    {
        public string CourseName { get; set; }
        public string UniversityId { get; set; }
        public int MinGrade { get; set; }
        public string CourseId { get; set; }
        public string CourseDescription { get; set; }
        public int Group { get; set; }
        public List<StudentInCourse> Students { get; set; }
    }
}
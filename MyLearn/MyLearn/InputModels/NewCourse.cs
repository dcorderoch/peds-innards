using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class NewCourse
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ProfUserId { get; set; }
        public string UniversityId { get; set; }
        public int Group { get; set; }
        public int MinGrade { get; set; }
        public List<Badge> Badges { get; set; }
    }
}
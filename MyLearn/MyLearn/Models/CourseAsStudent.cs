using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class CourseAsStudent
    {
        public string CourseName { get; set; }
        public string StudentUserId { get; set; }
        public string ProfUserId { get; set; }
        public string ProfessorName { get; set; }
        public string UniversityId { get; set; }
        public int Grade { get; set; }
        public List<Badge> Badges { get; set; }
        public string CourseId { get; set; }
        public string CourseDescription { get; set; }
        public int Group { get; set; }
        public int CourseState { get; set; }
    }
}
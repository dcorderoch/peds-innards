using System.Collections.Generic;

namespace MyLearn.Models
{
    public class CourseAsStudent
    {
        public string CourseName { get; set; }
        public string StudentUserId { get; set; }
        public string ProfUserId { get; set; }
        public string ProfessorName { get; set; }
        public string UniversityId { get; set; }
        public float Grade { get; set; }
        public List<Badge> Badges { get; set; }
        public string CourseId { get; set; }
        public string CourseDescription { get; set; }
        public float Group { get; set; }
        public float CourseState { get; set; }
    }
}
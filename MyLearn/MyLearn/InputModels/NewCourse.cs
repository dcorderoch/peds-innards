using System.Collections.Generic;

namespace MyLearn.InputModels
{
    public class NewCourse
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ProfUserId { get; set; }
        public string UniversityId { get; set; }
        public string Group { get; set; }
        public decimal MinGrade { get; set; }
        public List<Models.Badge> Badges { get; set; }
    }
}
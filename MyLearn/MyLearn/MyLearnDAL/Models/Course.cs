using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Course")]
    public class Course
    {
        public Course()
        {
            StudentCourses = new List<StudentCourse>();
            CourseTechnologies= new List<CourseTechnology>();
            CourseAchievements= new List<CourseAchievement>();
            
        }

        [Key]
        public Guid CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Group { get; set; }
        [Required]
        [ForeignKey("Professor")]
        public Guid ProfessorId { get; set; }
        [Required]
        [ForeignKey("University")]
        public Guid UniversityId { get; set; }
        [Required]
        public decimal MinScore { get; set; }
        [Required]
        public int IsActive { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual University University { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
        public virtual List<CourseTechnology> CourseTechnologies { get; set; }
        public virtual List<CourseAchievement> CourseAchievements { get; set; }
    }
}

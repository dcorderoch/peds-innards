using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }
        [Key]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}

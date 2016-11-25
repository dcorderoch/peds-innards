using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("CourseTechnology")]
    public class CourseTechnology
    {
        [Key]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }
        [Key]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        public virtual Technology Technology { get; set; }
        public virtual Course Course { get; set; }
    }
}

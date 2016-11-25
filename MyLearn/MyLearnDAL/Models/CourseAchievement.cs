

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("CourseAchievement")]
    public class CourseAchievement
    {
        [Key]
        [ForeignKey("Achievement")]
        public Guid AchievementId { get; set; }
        [Key]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}

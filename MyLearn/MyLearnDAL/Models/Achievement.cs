
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Achievement")]
    public class Achievement
    {
        [Key]
        public Guid AchievementId {get; set; }
        [Required]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Score { get; set; }

        public virtual Course Course { get; set; }
    }
}



using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Badge")]
    public class Badge
    {
        [Key]
        public Guid BadgeId { get; set; }
        [Required]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        [Required]
        [ForeignKey("Achievement")]
        public Guid AchievementId { get; set; }
        [Required]
        public int Bragged { get; set; }

        public virtual Project Project { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}

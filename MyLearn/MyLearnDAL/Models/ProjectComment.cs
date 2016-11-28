

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("ProjectComment")]
    public class ProjectComment
    {
        [Key]
        public Guid CommentId { get; set; }
        [Required]
        public Guid ParentId { get; set; }
        [Required]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}

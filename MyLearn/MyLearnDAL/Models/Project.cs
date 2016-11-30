using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("Project")]
    public class Project
    {
        public Project()
        {
            Technologies= new List<Technology>();
            ProjectComments= new List<ProjectComment>();
            Badges =new List<Badge>();
        }
        [Key]
        public Guid ProjectId { get; set; }
        [ForeignKey("Student")]
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("Course")]
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public int IsActive { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Score { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual List<Technology> Technologies { get; set; }
        public virtual List<ProjectComment> ProjectComments { get; set; }
        public virtual List<Badge> Badges { get; set; }
    }
}

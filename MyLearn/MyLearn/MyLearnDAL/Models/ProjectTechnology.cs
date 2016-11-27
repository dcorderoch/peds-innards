using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("ProjectTechnology")]
    public class ProjectTechnology
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Technology Technology { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("ProjectTechnology")]
    public class ProjectTechnology
    {
        [Key]
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        [Key]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Technology Technology { get; set; }
    }
}

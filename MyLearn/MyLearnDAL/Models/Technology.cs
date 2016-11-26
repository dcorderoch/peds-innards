using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Tecnology")]
    public class Technology
    {
        public Technology()
        {
            StudentTechnologies =new List<StudentTechnology>();
            CourseTechnologies= new List<CourseTechnology>();
            JobOfferTechnologies= new List<JobOfferTechnology>();
            ProjectTechnologies=new List<ProjectTechnology>();
        }
        [Key]
        public Guid TecnologyId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<StudentTechnology> StudentTechnologies { get; set; }
        public virtual List<CourseTechnology>  CourseTechnologies { get; set; }
        public virtual List<JobOfferTechnology> JobOfferTechnologies { get; set; }
        public virtual List<ProjectTechnology> ProjectTechnologies { get; set; }
    }
}

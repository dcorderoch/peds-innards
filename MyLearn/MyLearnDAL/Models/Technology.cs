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
            Students =new List<Student>();
            Courses =new List<Course>();
            JobOffers= new List<JobOffer>();
            Projects=new List<Project>();
        }
        [Key]
        public Guid TecnologyId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Course>  Courses { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}

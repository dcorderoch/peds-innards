using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("University")]
    public class University
    {
        public University()
        {
            Courses = new List<Course>();
        }

        [Key]
        public Guid UniversityId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Course> Courses { get; set; }
    }
}

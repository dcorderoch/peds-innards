using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearnDAL.Models
{
    [Table("Professor")]
    public class Professor : User
    {
        public Professor()
        {
            Courses = new List<Course>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(30)]
        public string ProfessorId { get; set; }
        [Required]
        public string Schedule { get; set; }
        [ForeignKey("University")]
        public Guid UniversityId { get; set; }

        public virtual University University { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}

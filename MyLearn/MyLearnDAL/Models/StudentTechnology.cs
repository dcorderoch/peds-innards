using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("StudentTechnology")]
    public class StudentTechnology
    {
        [Key]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }
        [Key]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }

        public virtual Technology Technology { get; set; }
        public virtual Student Student { get; set; }
    }
}

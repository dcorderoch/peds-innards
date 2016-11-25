using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("StudentTechnology")]
    public class StudentTechnology
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }

        public virtual Technology Technology { get; set; }
        public virtual Student Student { get; set; }
    }
}

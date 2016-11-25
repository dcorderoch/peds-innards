

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("StudentLanguage")]
    public class StudentLanguage
    {
        [Key]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }
        [Key]
        [ForeignKey("Language")]
        public int LanguageId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Language Language { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("Language")]
    public class Language
    {
        public Language()
        {
            StudentLanguages = new List<StudentLanguage>();
        }
        [Key]
        public int LenguageId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<StudentLanguage> StudentLanguages { get; set; }
    }
}

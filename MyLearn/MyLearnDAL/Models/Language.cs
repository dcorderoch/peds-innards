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
            Students = new List<Student>();
        }
        [Key]
        public int LenguageId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}

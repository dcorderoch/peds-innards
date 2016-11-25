using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    [Table("Language")]
    public class Language
    {
        [Key]
        public int LenguageId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

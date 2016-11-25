using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

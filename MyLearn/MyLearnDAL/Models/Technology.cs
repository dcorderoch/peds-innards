using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Tecnology")]
    public class Technology
    {
        [Key]
        public Guid TecnologyId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

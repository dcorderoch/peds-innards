using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("Tecnology")]
    public class Technology
    {
        public Guid TecnologyId { get; set; }
        public string Name { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("JobOfferTechnology")]
    public class JobOfferTechnology
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("JobOffer")]
        public Guid JobOfferId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Technology")]
        public Guid TechnologyId { get; set; }

        public virtual JobOffer JobOffer { get; set; }
        public virtual Technology Technology { get; set; }
    }
}

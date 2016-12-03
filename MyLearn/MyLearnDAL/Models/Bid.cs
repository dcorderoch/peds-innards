

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    /// <summary>
    /// Bid database model
    /// </summary>
    [Table("Bid")]
    public class Bid
    {
        [Key]
        public Guid BidId { get; set; }
        [Required]
        [ForeignKey("JobOffer")]
        public Guid JobOfferId { get; set; }
        [Required]
        [ForeignKey("Student")]
        public Guid UserId { get; set; }
        [Required]
        public decimal Money { get; set; }
        [Required]
        public int Duration { get; set; }

        public virtual JobOffer JobOffer { get; set; }
        public virtual Student Student { get; set; }
    }
}

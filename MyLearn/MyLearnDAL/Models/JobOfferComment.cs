using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    /// <summary>
    /// JobOfferComment database model
    /// </summary>
    [Table("JobOfferComment")]
    public class JobOfferComment { 
        [Key]
        public Guid CommentId { get; set; }
        [Required]
        public Guid ParentId { get; set; }
        [Required]
        [ForeignKey("JobOffer")]
        public Guid JobOfferId { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        public string File { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual JobOffer JobOffer { get; set; }
        public virtual User User { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("JobOffer")]
    public class JobOffer
    {
        public JobOffer()
        {
            Comments = new List<JobOfferComment>();
            Technologies= new List<Technology>();
            Bids = new List<Bid>();
        }

        [Key]
        public Guid JobOfferId { get; set; }
        [Required]
        [ForeignKey("Employer")]
        public Guid EmployerId { get; set; }
        [ForeignKey("Student")]
        public Guid? UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public int IsActive { get; set; }
        [Required]
        public string Description { get; set; }
        public string StateDescription { get; set; }

        public virtual Employer Employer { get; set; }
        public virtual Student Student { get; set; }
        public virtual List<JobOfferComment> Comments { get; set; }
        public virtual List<Technology> Technologies { get; set; }
        public virtual List<Bid> Bids { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLearnDAL.Models
{
    [Table("JobOffer")]
    public class JobOffer
    {
        public JobOffer()
        {
            JobOfferComments = new List<JobOfferComment>();
            Technologies = new List<Technology>();
            Bids = new List<Bid>();
        }

        public Guid JobOfferId { get; set; }

        public Guid EmployerId { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Budget { get; set; }

        public int IsActive { get; set; }

        public virtual Employer Employer { get; set; }
        public virtual Student Student { get; set; }
        public virtual List<JobOfferComment> JobOfferComments { get; set; }
        public virtual List<Technology> Technologies { get; set; }
        public virtual List<Bid> Bids { get; set; }
    }
}

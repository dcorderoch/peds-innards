using System.Collections.Generic;

namespace MyLearn.Models
{
    public class AllJobOffersByEmployer
    {
        public List<ActiveJobOffer> ActiveJobOffers { get; set; }
        public List<FinishedJobOffer> FinishedJobOffers { get; set; }
    }
}
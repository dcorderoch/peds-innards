using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class AllJobOffersByEmployer
    {
        public List<ActiveJobOffersList> ActiveJobOffers { get; set; }
        public List<FinishedJobOffer> FinishedJobOffers { get; set; }
    }
}
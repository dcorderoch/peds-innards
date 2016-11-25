using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class AllJobOffers
    {
        public ActiveJobOffersList ActiveJobOffers { get; set; }
        public FinishedJobOffersList FinishedJobOffers { get; set; }
    }
}

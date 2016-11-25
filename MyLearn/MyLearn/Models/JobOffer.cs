using System.Collections.Generic;

namespace MyLearn.Models
{
    public class JobOffer
    {
        public string JobOfferTitle { get; set; }
        public List<string> Technologies { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public int IsActive { get; set; }
    }
}
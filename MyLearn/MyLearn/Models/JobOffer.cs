using System.Collections.Generic;

namespace MyLearn.Models
{
    public class JobOffer
    {
        public string JobOfferTitle { get; set; }
        public string JobOfferId { get; set; }
        public string EmployerUserId { get; set; }
        public string EmployerName { get; set; }
        public List<string> Technologies { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public int State { get; set; }
        public string StateDescription { get; set; }
    }
}
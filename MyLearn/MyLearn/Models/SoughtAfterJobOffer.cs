using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class SoughtAfterJobOffer
    {
        public string EmployerUserId { get; set; }
        public string EmployerName { get; set; }
        public string JobOffer { get; set; }
        public string JobOfferId { get; set; }
        public List<string> Technologies { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public string State { get; set; }
        public string StateDescription { get; set; }
    }
}
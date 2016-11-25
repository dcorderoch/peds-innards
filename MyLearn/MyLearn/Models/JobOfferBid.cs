using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class JobOfferBid
    {
        public string Money { get; set; }
        public int DurationInDays { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentUserId { get; set; }
    }
}
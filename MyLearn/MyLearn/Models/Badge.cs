using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class Badge
    {
        public string BadgeDescription { get; set; }
        public int Value { get; set; }
        public int Awarded { get; set; }
        public int Alardeado { get; set; }
    }
}
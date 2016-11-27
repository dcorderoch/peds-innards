using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class CloseJobOffer
    {
        public string JobOfferId { get; set; }
        public int State { get; set; }
        public string StateDescription { get; set; }
    }
}
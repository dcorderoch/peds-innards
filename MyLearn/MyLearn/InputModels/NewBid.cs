﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class NewBid
    {
        public string JobOfferId { get; set; }
        public decimal Money { get; set; }
        public string DurationDays { get; set; }
        public string StudentSurname { get; set; }
        public string StudentUserId { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class ActiveCourse
    {
        public string course { get; set; }
        public string CourseId { get; set; }
        public string CourseDescription { get; set; }
        public int accepted { get; set; }
    }
}
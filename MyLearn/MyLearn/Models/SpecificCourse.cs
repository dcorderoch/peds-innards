﻿using System.Collections.Generic;

namespace MyLearn.Models
{
    public class SpecificCourse
    {
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public decimal Grade { get; set; }
        public List<Badge> Badges { get; set; }
    }
}
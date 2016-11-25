using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.Models
{
    public class SpecificCourse
    {
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public int Grade { get; set; }
        public List<Badge> Badges { get; set; }
    }
}
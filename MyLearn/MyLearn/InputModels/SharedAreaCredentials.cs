using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class SharedAreaCredentials
    {
        public string StudentUserId { get; set; }
        public string ProfUserId { get; set; }
        public string UniversityId { get; set; }
        public string Group { get; set; }
        public string CourseId { get; set; }
    }
}
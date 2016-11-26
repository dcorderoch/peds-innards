using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class ProjectProposal
    {
        public string ProjectName { get; set; }
        public List<string> Technologies { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public List<string> OtherFiles { get; set; }
        public string StudentUserId { get; set; }
        public string CourseId { get; set; }
    }
}
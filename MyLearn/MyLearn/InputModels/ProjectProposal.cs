using System.Collections.Generic;

namespace MyLearn.InputModels
{
    public class ProjectProposal
    {
        public string ProjectName { get; set; }
        public List<string> Technologies { get; set; }
        public string Description { get; set; }
        public List<string> OtherFiles { get; set; }
        public string StudentUserId { get; set; }
        public string CourseId { get; set; }
    }
}
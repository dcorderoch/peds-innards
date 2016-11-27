using System.Collections.Generic;

namespace MyLearn.InputModels
{
    public class NewJobOffer
    {
        public string JobOffer { get; set; }
        public List<string> Technologies { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
    }
}
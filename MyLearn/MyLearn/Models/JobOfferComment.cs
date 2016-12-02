using System.Collections.Generic;

namespace MyLearn.Models
{
    public class JobOfferComment
    {
        public string CommentId { get; set; }
        public string ParentId { get; set; }
        public string Date { get; set; }
        public int IsFromStudent { get; set; }
        public string CommentContent { get; set; }
        public List<JobOfferComment> NestedComments { get; set; }
        public string File { get; set; }
    }
}
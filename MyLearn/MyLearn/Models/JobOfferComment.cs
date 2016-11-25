using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
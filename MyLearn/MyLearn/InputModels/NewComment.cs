using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class NewComment
    {
        public string StudentId { get; set; }
        public string ProfUserId { get; set; }
        public string CourseId { get; set; }
        public string Comment { get; set; }
        public string ParentId { get; set; }
        public int Commenter { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLearn.InputModels
{
    public class NewCommentWithFile
    {
        public string StudentUserId { get; set; }
        public string ProfUserId { get; set; }
        public string CourseId { get; set; }
        public string Comment { get; set; }
        public string ParentId { get; set; }
        public int Commenter { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public string RefreshToken { get; set; }
    }
}
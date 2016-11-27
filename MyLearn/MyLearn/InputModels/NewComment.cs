namespace MyLearn.InputModels
{
    public class NewComment
    {
        public string StudentId { get; set; }
        public string ProfUserId { get; set; }
        public string CourseId { get; set; }
        public string Comment { get; set; }
        public string ParentId { get; set; }
        public float Commenter { get; set; }
    }
}
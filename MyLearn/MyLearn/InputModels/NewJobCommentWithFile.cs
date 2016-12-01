namespace MyLearn.InputModels
{
    public class NewJobCommentWithFile
    {

        public string StudentUserId { get; set; }
        public string EmployerUserId { get; set; }
        public string JobOfferId { get; set; }
        public string JobOfferComment { get; set; }
        public string ParentId { get; set; }
        public int Commenter { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public string RefreshToken { get; set; }

    }
}
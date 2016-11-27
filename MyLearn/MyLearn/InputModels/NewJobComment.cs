namespace MyLearn.InputModels
{
    public class NewJobComment
    {
        public string JobOfferId { get; set; }
        public string JobOfferComment { get; set; }
        public string ParentId { get; set; }
        public int Commenter { get; set; }
    }
}
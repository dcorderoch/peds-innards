namespace MyLearn.InputModels
{
    public class CloseJobOffer
    {
        public string JobOfferId { get; set; }
        public int State { get; set; }
        public string StateDescription { get; set; }
        public int Stars { get; set; }
    }
}
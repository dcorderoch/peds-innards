namespace MyLearn.InputModels
{
    public class CustomTopStudent
    {
        public int NumberOfTopStudents { get; set; }
        public string CountryId { get; set; }
        public decimal CourseAvgWeight { get; set; }
        public decimal CourseSuccessRateWeight { get; set; }
        public decimal ProjectAvgWeight { get; set; }
        public decimal ProjectSuccessRateWeight { get; set; }
    }
}
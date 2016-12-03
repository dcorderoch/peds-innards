
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    /// <summary>
    /// Employer database model
    /// </summary>
    [Table("Employer")]
    public class Employer : User
    {
        public Employer()
        {
            JobOffers = new List<JobOffer>();
        }

        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(30)]
        public string ContactName { get; set; }
        [Required]
        [MaxLength(30)]
        public string EmployerId { get; set; }
        [Required]
        [MaxLength(30)]
        public string ContactLastname { get; set; }
        [MaxLength(30)]
        public string Website { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}

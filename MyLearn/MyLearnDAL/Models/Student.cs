using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyLearnDAL.Models
{
    /// <summary>
    /// Student model
    /// </summary>
    [Table("Student")]
    public class Student : User
    {

        public Student()
        {
            Projects = new List<Project>();
            JobOffers = new List<JobOffer>();
            Bids = new List<Bid>();
            Languages = new List<Language>();
            Technologies = new List<Technology>();
            Courses = new List<Course>();
            Notifications = new List<Notification>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public string CardId { get; set; }
        public string RepoLink { get; set; }
        public string ResumeLink { get; set; }
        [ForeignKey("University")]
        [Required]
        public Guid UniversityId { get; set; }
        [Required]
        public decimal AvgProjects { get; set; }
        [Required]
        public decimal AvgCourses { get; set; }
        [Required]
        public int NumSuceedCourses { get; set; }
        [Required]
        public int NumFailedCourses { get; set; }
        [Required]
        public int NumSuceedProjects { get; set; }
        [Required]
        public int NumFailedProjects { get; set; }

        public virtual University University { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<Bid> Bids { get; set; }
        public virtual List<Language> Languages { get; set; }
        public virtual List<Technology> Technologies { get; set; }
        public virtual List<Course> Courses { get; set; }
        public virtual List<Notification> Notifications { get; set; }
    }
}

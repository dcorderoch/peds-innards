using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearnDAL.Models
{
    public class Student
    {

        public Student()
        {
            Projects = new List<Project>();
            JobOffers = new List<JobOffer>();
            StudentBids = new List<StudentBid>();
            Languages = new List<Language>();
            Technologies = new List<Technology>();
            Courses = new List<Curse>();
            Notifications = new List<Notification>();
        }

        public virtual Guid StudentId  { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public byte[] Photo { get; set; }

        public DateTime InDate { get; set; }

        public string CardId { get; set; }

        public string RepoLink { get; set; }

        public string ResumeLink { get; set; }

        public virtual Guid UniversityId { get; set; }

        public decimal AvgProjects { get; set; }

        public decimal AvgCourses { get; set; }

        public int NumSuceedCourses { get; set; }

        public int NumFailedProjects { get; set; }

        public virtual List<Project> Projects { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }

        public virtual List<StudentBid> StudentBids { get; set; }

        public virtual List<Language> Languages { get; set; }

        public virtual List<Technology> Technologies { get; set; }

        public virtual List<Curse> Courses { get; set; }

        public virtual List<Notification> Notifications { get; set; }








    }
}

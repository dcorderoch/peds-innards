using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using MyLearnDAL.Models;

namespace MyLearnDAL
{
    using System.Data.Entity;

    public class MyLearnContext : DbContext
    {

        public MyLearnContext()
            : base("name=MyLearnContext")
        {
            Database.SetInitializer<MyLearnContext>(new MyLearnContextInitializer());
        }

        // DBsets

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAchievement> CourseAchievements { get; set; }
        public virtual DbSet<CourseTechnology> CourseTechnologies { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<JobOffer> JobOffers { get; set; }
        public virtual DbSet<JobOfferComment> JobOfferComments { get; set; }
        public virtual DbSet<JobOfferTechnology> JobOfferTechnologies { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectComment> ProjectComments { get; set; }
        public virtual DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<StudentLanguage> StudentLanguages { get; set; }
        public virtual DbSet<StudentTechnology> StudentTechnologies { get; set; }
        public virtual DbSet<Technology> Technologies { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<User> User { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
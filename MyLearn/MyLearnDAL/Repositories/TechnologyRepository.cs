
using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class TechnologyRepository : Repository<Technology>
    {
        public TechnologyRepository(MyLearnContext context) : base(context) { }

        public Technology GetTechnologybyId(Guid techId)
        {
            return DbSet.Find(techId);
        }

        public List<Technology> GetStudentTechnologies(Guid UserId)
        {
            return DbSet.Where(t => t.Students.Any(s => s.UserId.Equals(UserId))).ToList();
        }

        public List<Technology> GetProjectTechnologies(Guid ProjectId)
        {
            return DbSet.Where(t => t.Projects.Any(p => p.ProjectId.Equals(ProjectId))).ToList();
        }

        public List<Technology> GetJobOfferTechnologies(Guid JobOfferId)
        {
            return DbSet.Where(t => t.JobOffers.Any(j => j.JobOfferId.Equals(JobOfferId))).ToList();
        }

        public List<Technology> GetCourseYTechnologies(Guid CourseId)
        {
            return DbSet.Where(t => t.Courses.Any(c => c.CourseId.Equals(CourseId))).ToList();
        }
    }
}

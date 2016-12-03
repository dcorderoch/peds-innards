
using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Technology repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class TechnologyRepository : Repository<Technology>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public TechnologyRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get a technology for the given id
        /// </summary>
        /// <param name="techId"></param>
        /// <returns>A technology</returns>
        public Technology GetTechnologybyId(Guid techId)
        {
            return DbSet.Find(techId);
        }

        /// <summary>
        /// Get a technology for the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of technologies</returns>
        public List<Technology>  GetTechnologiesByName(string name)
        {
            return DbSet.Where(t => t.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        /// <summary>
        /// Get student's technologies by the student id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>A list of technologies</returns>
        public List<Technology> GetStudentTechnologies(Guid UserId)
        {
            return DbSet.Where(t => t.Students.Any(s => s.UserId.Equals(UserId))).ToList();
        }

        /// <summary>
        /// Get project's technologies by the project id
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>List of thechnologies</returns>
        public List<Technology> GetProjectTechnologies(Guid ProjectId)
        {
            return DbSet.Where(t => t.Projects.Any(p => p.ProjectId.Equals(ProjectId))).ToList();
        }

        /// <summary>
        /// Get job offer's technologies by the joboffer id
        /// </summary>
        /// <param name="JobOfferId"></param>
        /// <returns>A list of technologies</returns>
        public List<Technology> GetJobOfferTechnologies(Guid JobOfferId)
        {
            return DbSet.Where(t => t.JobOffers.Any(j => j.JobOfferId.Equals(JobOfferId))).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class JobOfferRepository : Repository<JobOffer>
    {
        public JobOfferRepository(MyLearnContext context) : base(context) { }

        public JobOffer GetJobOfferById(Guid jobOfferId)
        {
            return DbSet.Find(jobOfferId);
        }

        public List<JobOffer> GetEmployerJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Employer.UserId.Equals(userId)).ToList();
        }

        public List<JobOffer> GetEmployerActiveJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Employer.UserId.Equals(userId) && (j.IsActive.Equals(0) || j.IsActive.Equals(1))).ToList();
        }

        public List<JobOffer> GetEmployerInactiveJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Employer.UserId.Equals(userId) && (j.IsActive.Equals(2) || j.IsActive.Equals(3))).ToList();
        }

        public List<JobOffer> GetStudentJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(userId)).ToList();
        }

        public List<JobOffer> GetStudentActiveJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(userId) && j.IsActive.Equals(1)).ToList();
        }

        public List<JobOffer> GetStudentInactiveJobOffers(Guid userId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(userId) && (j.IsActive.Equals(2) || j.IsActive.Equals(3))).ToList();
        }

        public List<JobOffer> GetJobOfferByTechnology(Guid techId)
        {
            return DbSet.Where(j => j.Technologies.Any(t => t.TechnologyId.Equals(techId))).ToList();
        }

        public List<JobOffer> GetJobOfferByName(string name)
        {
            return DbSet.Where(j => j.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}

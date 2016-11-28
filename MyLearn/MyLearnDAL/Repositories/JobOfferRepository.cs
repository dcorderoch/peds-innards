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

        public JobOffer GetJobOfferById(Guid jobOfferId)
        {
            return DbSet.Find(jobOfferId);
        }

        public List<JobOffer> GetStudentJobOffers(Guid UserId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(UserId)).ToList();
        }

        public List<JobOffer> GetStudentActiveJobOffers(Guid UserId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(UserId) && j.IsActive.Equals(1)).ToList();
        }

        public List<JobOffer> GetStudentInactiveJobOffers(Guid UserId)
        {
            return DbSet.Where(j => j.Student.UserId.Equals(UserId) && (j.IsActive.Equals(2) || j.IsActive.Equals(3))).ToList();
        }
    }
}

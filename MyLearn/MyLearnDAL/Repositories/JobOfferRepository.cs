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
    }
}

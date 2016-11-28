using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class JobOfferCommentRepository: Repository<JobOfferComment>
    {
        public List<JobOfferComment> GetJobOfferCommentsByJobOfferId(Guid jobOfferId)
        {
            return DbSet.Where(joc => joc.JobOffer.JobOfferId.Equals(jobOfferId)).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class BidRepository : Repository<Bid>
    {
        public BidRepository(MyLearnContext context) : base(context) { }

        public Bid GetBidById(Guid bidId)
        {
            return DbSet.Find(bidId);
        }

        public List<Bid> GetStudentBids(Guid UserId)
        {
            return DbSet.Where(b => b.Student.UserId.Equals(UserId)).ToList();
        }

        public List<Bid> GetJobOfferBids(Guid JobOfferId)
        {
            return DbSet.Where(b => b.JobOffer.JobOfferId.Equals(JobOfferId)).ToList();
        }
    }
}

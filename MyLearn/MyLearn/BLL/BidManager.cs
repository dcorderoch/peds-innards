using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class BidManager
    {
        public ReturnCode Create(NewBid newBid)
        {
            using (var context = new MyLearnContext())
            {
                var retVal = new ReturnCode();
                var bidRepo = new BidRepository(context);
                if (newBid.Money <= 0)
                {
                    retVal.ReturnStatus = 0;
                }
                else
                {
                    var bid = new Bid
                    {
                        BidId = Guid.NewGuid(),
                        UserId = new Guid(newBid.StudentUserId),
                        JobOfferId = new Guid(newBid.JobOfferId),
                        Money = newBid.Money,
                        Duration = Convert.ToInt32(newBid.DurationDays)
                    };
                    bidRepo.Add(bid);
                    bidRepo.SaveChanges();
                    retVal.ReturnStatus = 1;
                }
                bidRepo.Dispose();
                return retVal;
            }
        }
        public ReturnCode Update(NewBid newBid)
        {
            using (var context = new MyLearnContext())
            {
                var retVal = new ReturnCode();
                BidRepository bidRepo = new BidRepository(context);
                List<Bid> bids = bidRepo.GetStudentBids(new Guid(newBid.StudentUserId));
                Bid bid = bids.Find(x => x.JobOfferId.ToString() == newBid.JobOfferId);
                if (bid != null)
                {
                    bid.Duration = Convert.ToInt32(newBid.DurationDays);
                    bid.Money = newBid.Money;
                    retVal.ReturnStatus = 1;
                }
                return retVal;
            }
        }
    }
}
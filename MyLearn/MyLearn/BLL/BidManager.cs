using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class BidManager
    {
        public ReturnCode Create(NewBid newBid)
        {
            var retVal = new ReturnCode();
            BidRepository bidRepo = new BidRepository();
            Bid bid = new Bid();
            if (bid.Money <= 0)
            {
                retVal.ReturnStatus = 0;
            }
            else
            {
                bid.BidId = Guid.NewGuid();
                bid.UserId = new Guid(newBid.StudentUserId);
                bid.JobOfferId = new Guid(newBid.JobOfferId);
                bid.Money = newBid.Money;
                bid.Duration = Convert.ToInt32(newBid.DurationDays);
                bidRepo.Add(bid);
                bidRepo.SaveChanges();
                retVal.ReturnStatus = 1;
            }
            bidRepo.Dispose();
            return retVal;
        }
        public ReturnCode Update(NewBid newBid)
        {
            var retVal = new ReturnCode();
            BidRepository bidRepo = new BidRepository();
            List<Bid> bids = bidRepo.GetStudentBids(new Guid(newBid.StudentUserId));
            Bid bid = bids.Find(x => x.JobOfferId.ToString()==newBid.JobOfferId);
            if(bid != null)
            {
                bid.Duration = Convert.ToInt32(newBid.DurationDays);
                bid.Money = newBid.Money;
                retVal.ReturnStatus = 1;
            }
            return retVal;
        }
    }
}
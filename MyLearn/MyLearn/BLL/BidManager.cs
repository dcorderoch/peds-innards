using System;
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

            bid.BidId = Guid.NewGuid();
            bid.UserId = new Guid(newBid.StudentUserId);
            bid.JobOfferId = new Guid(newBid.JobOfferId);
            bid.Money = newBid.Money;
            bid.Duration = Convert.ToInt32(newBid.DurationDays);
/*            
        public int Duration { get; set; }*/

        retVal.ReturnStatus = 1;
            return retVal;
        }
        public ReturnCode Update(NewBid newBid)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }
    }
}
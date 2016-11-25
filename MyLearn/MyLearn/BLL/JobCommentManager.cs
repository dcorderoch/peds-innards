using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.InputModels;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class JobCommentManager
    {
        public List<JobOfferComment> GetAllComments(string jobOfferId)
        {
            List<JobOfferComment> allComments = new List<JobOfferComment>();

            return allComments;

        }

        public ReturnCode CreateComment(NewJobComment newJobComment)
        {
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
    }
}
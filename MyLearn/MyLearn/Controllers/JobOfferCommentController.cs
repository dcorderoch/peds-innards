﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class JobOfferCommentController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewJobComment jobComment)
        {
            var jobCmntMngr = new JobCommentManager();
            var retVal = jobCmntMngr.CreateComment(jobComment);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOfferComment>> GetAll(JobOfferIdentifier jobOfferId)
        {
            var jobCmntMngr = new JobCommentManager();
            var retVal = jobCmntMngr.GetAllComments(jobOfferId.JobOfferId);
            return Json(retVal);
        }
    }
}

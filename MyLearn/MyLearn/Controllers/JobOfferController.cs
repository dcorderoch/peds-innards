﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class JobOfferController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewJobOffer newJobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.CreateJobOffer(newJobOffer);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Assign(AssignJobOffer jobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.Assign(jobOffer);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<ReturnCode> Close(CloseJobOffer openJobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.CloseJobOffer(openJobOffer);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<AllJobOffersByEmployer> GetByEmployer(EmployerIdentifier employerID)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByEmployer(employerID.EmployerUserId);
            if (retVal == null)
            {
                retVal = new AllJobOffersByEmployer();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<JobOffer> GetById(JobOfferIdentifier jobId)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffer(jobId.JobOfferId);
            if (retVal == null)
            {
                retVal = new JobOffer();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOffer>> GetByTechnology(JobOfferByTechnology technology)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByTechnology(technology.Technology);
            if (retVal == null)
            {
                retVal = new List<JobOffer>();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOffer>> GetByName(JobOfferByName jobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByName(jobOffer.JobOffer);
            if (retVal == null)
            {
                retVal = new List<JobOffer>();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOfferBid>> GetBidsById(JobOfferIdentifier jobId)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetBidsById(jobId.JobOfferId);
            if (retVal == null)
            {
                retVal = new List<JobOfferBid>();
            }
            return Json(retVal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<JobOffer> GetById(JobOfferIdentifier jobId)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffer(jobId.JobOfferId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOffer>> GetByTechnology(Technology technology)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByTechnology(technology.TechnologyId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOffer>> GetByName(Technology technology)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByName(technology.TechnologyId);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<List<JobOfferBid>> GetBidsById(JobOfferIdentifier jobId)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetBidsById(jobId.JobOfferId);
            return Json(retVal);
        }
    }
}

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class JobOfferController : ApiController
    {
        /// <summary>
        /// API Method to post a new Job Offer by an Employer in the MyLearn Database
        /// </summary>
        /// <param name="newJobOffer"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Create(NewJobOffer newJobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.CreateJobOffer(newJobOffer);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to assign a Job Offer to a Student
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Assign(AssignJobOffer jobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.Assign(jobOffer);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to Close a Job Offer, and mark it as successful or failed
        /// </summary>
        /// <param name="openJobOffer"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<ReturnCode> Close(CloseJobOffer openJobOffer)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.CloseJobOffer(openJobOffer);
            return Json(retVal);
        }
        /// <summary>
        /// API Method to get all job offers posted by an Employer
        /// </summary>
        /// <param name="employerID"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all the information of a job offer posted by an Employer, minus bids (see GetBidsById() below)
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all job offers posted that include/require a particular technology
        /// </summary>
        /// <param name="technology"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all job offers posted with a particular Name
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to get all the bids on a job offer auction posted by an Employer
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<AllStudentJobOffer> GetByStudent(StudentIdentifier StudentId)
        {
            var JOMngr = new JobManager();
            var retVal = JOMngr.GetJobOffersByStudent(StudentId.StudentUserId);
            if (retVal == null)
            {
                retVal = new AllStudentJobOffer();
            }
            return Json(retVal);
        }
    }
}

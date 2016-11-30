using System;
using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using JobOffer = MyLearn.Models.JobOffer;

namespace MyLearn.BLL
{
    public class JobManager
    {
        public ReturnCode CreateJobOffer(NewJobOffer newOffer)
        {
            using (var context = new MyLearnContext())
            {
                var jobOfferRepo = new JobOfferRepository(context);
                var jobOffer = new MyLearnDAL.Models.JobOffer();
                var returnCode = new ReturnCode();
                returnCode.ReturnStatus = 0;

                jobOffer.JobOfferId = Guid.NewGuid();
                jobOffer.EmployerId = new Guid(newOffer.EmployerId);
                jobOffer.Name = newOffer.JobOffer;
                jobOffer.StartDate = DateTime.Parse(newOffer.StartDate);
                jobOffer.EndDate = DateTime.Parse(newOffer.EndDate);
                jobOffer.Budget = newOffer.Budget;
                jobOffer.IsActive = 0;
                jobOffer.StateDescription = "";
                try
                {
                    jobOfferRepo.Add(jobOffer);
                    jobOfferRepo.SaveChanges();
                    returnCode.ReturnStatus += 1;
                }
                catch (Exception)
                {
                    returnCode.ReturnStatus = 0;
                }

                if (returnCode.ReturnStatus != 1) return returnCode;
                var tweeter = new Tweeter();
                if (tweeter.tweet("Se ha creado la oferta de trabajo: " + jobOffer.Name + ".")) 
                {
                    returnCode.ReturnStatus += 1;
                }
                return returnCode;
            }
            
        }
        public ReturnCode Assign(AssignJobOffer jobOffer)
        {
            ReturnCode success = new ReturnCode();
            var retVal = new ReturnCode();
            // se hace el tweet ANTES de hacer lo demás, si el método retorna FALSE
            // se retorna que falló la vara
            var tweeter = new Tweeter();
            if (tweeter.tweet("mensaje")) //cambiar mensaje
            {
                retVal.ReturnStatus += 1;
                // marcar la maire como que ya se hizo alarde
            }
            retVal.ReturnStatus = 1;
            return retVal;
            
        }

        public ReturnCode CloseJobOffer(CloseJobOffer openJobOffer)
        {
            var retVal = new ReturnCode();
            // code goes here
            return retVal;
        }

        public AllJobOffersByEmployer GetJobOffersByEmployer(string employerId)
        {
            AllJobOffersByEmployer jobOffers = new AllJobOffersByEmployer();
            //get all professor's courses
            List<ActiveJobOffer> activeJobOffers = new List<ActiveJobOffer>();
            List<FinishedJobOffer> finishedJobOffers = new List<FinishedJobOffer>();
            //get from db and add them here, once the repositories are up and running

            //activeCourses = ;
            //finishedCourses = ;

            jobOffers.ActiveJobOffers = activeJobOffers;
            jobOffers.FinishedJobOffers = finishedJobOffers;

            return jobOffers;
        }

        public JobOffer GetJobOffer(string jobOfferId)
        {
            JobOffer jobOffer = new JobOffer();
            jobOffer.JobOfferTitle = "";
            jobOffer.Technologies = new List<string>();
            jobOffer.Location = "";
            jobOffer.StartDate = "";
            jobOffer.EndDate= "";
            jobOffer.Description= "";
            jobOffer.Budget = 1;
            return jobOffer;
            
        }

        public List<JobOffer> GetJobOffersByTechnology(string technology)
        {
            List<JobOffer> result = new List<JobOffer>();
            int n = 100;
            for (int i = 0; i < n; i++)
            {
                JobOffer jobOffer = new JobOffer();
                result.Add(jobOffer);
            }
            return result;
        }

        public List<JobOffer> GetJobOffersByName(string jobOfferTitle)
        {
            List<JobOffer> result = new List<JobOffer>();
            int n = 100;
            for (int i = 0; i < n; i++)
            {
                JobOffer jobOffer = new JobOffer();
                result.Add(jobOffer);
            }
            return result;
        }

        public List<JobOfferBid> GetBidsById(string jobOfferId)
        {
            List<JobOfferBid> bids = new List<JobOfferBid>();
            //get bids where jobOfferId matches
            return bids;
        }

    }
}
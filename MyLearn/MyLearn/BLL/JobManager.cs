using System.Collections.Generic;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;

namespace MyLearn.BLL
{
    public class JobManager
    {
        public ReturnCode CreateJobOffer(NewJobOffer newOffer)
        {
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
        public ReturnCode Assign(AssignJobOffer jobOffer)
        {
            ReturnCode success = new ReturnCode();
            success.ReturnStatus = 1;
            return success;
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
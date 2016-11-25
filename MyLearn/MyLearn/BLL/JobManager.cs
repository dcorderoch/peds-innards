using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.InputModels;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class JobManager
    {
        public ReturnCode CreateJobOffer(NewJobOffer newOffer)
        {
            ReturnCode success = new ReturnCode();

            //Create new offer
            //Add new course to DB
            /*dbobject.Add(newOffer.JobOffer);
            dbobject.Add(newOffer.Technologies);
            dbobject.Add(newOffer.Location);
            dbobject.Add(newOffer.StartDate);
            dbobject.Add(newOffer.EndDate);
            dbobject.Add(newOffer.Description);
            dbobject.Add(newOffer.Budget);*/

            success.StatusCode = 1;
            return success;
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
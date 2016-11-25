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

        public AllJobOffers GetJobOffersByEmployer(string employerId)
        {
            AllJobOffers jobOffers = new AllJobOffers();
            //get all employer's offers
            ActiveJobOffersList activeJobs = new ActiveJobOffersList();
            FinishedJobOffersList finishedJobs = new FinishedJobOffersList();
            //get from db
            activeJobs = new ActiveJobOffersList();
            finishedJobs = new FinishedCoursesList();

            allCourses.ActiveCourses = activeCourses;
            allCourses.FInishedCourses = finishedCourses;

            return allCourses;
        }
    }
}
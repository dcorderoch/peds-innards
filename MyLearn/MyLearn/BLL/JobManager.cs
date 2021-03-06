﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using MyLearn.Utils;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using JobOffer = MyLearn.Models.JobOffer;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class in charge of managing the job offers in MyLearn.
    /// </summary>
    public class JobManager
    {
        private ModelMapper mapper;

        public JobManager()
        {
        mapper= new ModelMapper();    
        }

        /// <summary>
        /// Method that creates a new job offer by an employer.
        /// </summary>
        /// <param name="newOffer"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
        public ReturnCode CreateJobOffer(NewJobOffer newOffer)
        {
            using (var context = new MyLearnContext())
            {
                var techRepo = new TechnologyRepository(context);
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
                jobOffer.Description = newOffer.Description;
                foreach (var tech in newOffer.Technologies)
                {
                    jobOffer.Technologies.Add(techRepo.GetTechnologybyId(Guid.Parse(tech)));
                }
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
                jobOfferRepo.Dispose();
                techRepo.Dispose();
                return returnCode;
            }    
        }

        /// <summary>
        /// Assigns a job offer to a winner student.
        /// </summary>
        /// <param name="jobOffer"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
        public ReturnCode Assign(AssignJobOffer jobOffer)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode success = new ReturnCode();
              
                var jobOfferRepo = new JobOfferRepository(context);
                var studentRepo= new StudentRepository(context);
                var notificationRepo= new NotificationRepository(context);
                var bidRepo = new BidRepository(context);

                var student = studentRepo.GetStudentById(Guid.Parse(jobOffer.StudentUserId));
                var currJobOffer = jobOfferRepo.GetJobOfferById(Guid.Parse(jobOffer.JobOfferId));

                if (student != null && currJobOffer != null)
                {
                    var notificationManager = new NotificationManager();
                    currJobOffer.IsActive = 1;
                    currJobOffer.UserId = Guid.Parse(jobOffer.StudentUserId);
                    notificationRepo.Add(notificationManager.CreateNotification(jobOffer.StudentUserId,
                        currJobOffer.Name));
                    bidRepo.RemoveAllBidsByJobOfferId(Guid.Parse(jobOffer.JobOfferId));
                    notificationRepo.SaveChanges();
                    bidRepo.SaveChanges();
                    success.ReturnStatus = 1;
                }
                else
                {
                    success.ReturnStatus = 0;
                }
                jobOfferRepo.Dispose();
                studentRepo.Dispose();
                bidRepo.Dispose();
                notificationRepo.Dispose();
                return success;
            }
        }
        /// <summary>
        /// Closes job offer.
        /// </summary>
        /// <param name="closeJobOffer"></param>
        /// <returns>Return code indicating whether or not the operation was successful.</returns>
        public ReturnCode CloseJobOffer(CloseJobOffer closeJobOffer)
        {
            using (var context = new MyLearnContext())
            {
                var retVal = new ReturnCode();
                try
                {
                    var jobOfferRepo = new JobOfferRepository(context);
                    var currentJobOffer = jobOfferRepo.GetJobOfferById(Guid.Parse(closeJobOffer.JobOfferId));
                    var student = currentJobOffer.Student;
                    var isActive = currentJobOffer.IsActive;
                    if (isActive == 1) //Fue asignada al estudiante
                    {
                        if (closeJobOffer.State == 2) //Finalizada exitosamente
                        {
                            currentJobOffer.IsActive = closeJobOffer.State;
                            currentJobOffer.StateDescription = closeJobOffer.StateDescription;
                            currentJobOffer.Score = closeJobOffer.Stars; //asignar nota del trabajo
                            student.AvgProjects = CalculateAverage(ObtainGrade(closeJobOffer.Stars),
                                    student.NumSuceedProjects + student.NumFailedProjects, student.AvgProjects); //actualizar promedio de estudiante
                            student.NumSuceedProjects += 1; //aumentar numero de proyectos exitosos
                            jobOfferRepo.SaveChanges(); 
                        }
                        else //Fracasó
                        {
                            currentJobOffer.IsActive = closeJobOffer.State;
                            currentJobOffer.StateDescription = closeJobOffer.StateDescription;
                            currentJobOffer.Score = closeJobOffer.Stars; //asignar nota del trabajo
                            student.AvgProjects = CalculateAverage(ObtainGrade(closeJobOffer.Stars),
                                    student.NumSuceedProjects + student.NumFailedProjects, student.AvgProjects); //actualizar promedio de estudiante
                            student.NumFailedProjects += 1; //aumentar numero de proyectos fracasados
                            jobOfferRepo.SaveChanges();
                        }
                    }
                    else //No fue asignada al estudiante
                    {
                        currentJobOffer.IsActive = closeJobOffer.State;
                        currentJobOffer.StateDescription = closeJobOffer.StateDescription;
                        currentJobOffer.Score = 0;
                        jobOfferRepo.SaveChanges();
                    }
                    retVal.ReturnStatus = 1;
                    jobOfferRepo.Dispose();
                }
                catch (Exception)
                {
                    retVal.ReturnStatus = 0; ;
                }
                return retVal;
            }
        }
        /// <summary>
        /// Auxiliary function that computes the grade given from stars to percent.
        /// </summary>
        /// <param name="stars"></param>
        /// <returns></returns>
        private int ObtainGrade(int stars)
        {
            return 20*stars;
        }
        /// <summary>
        /// Auxiliary method that calculates the average of projects for a student.
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="totalProjects"></param>
        /// <param name="currentAverage"></param>
        /// <returns>Average grade for a student.</returns>
        private decimal CalculateAverage(int grade, int totalProjects, decimal currentAverage)
        {
            var val = currentAverage*totalProjects + grade;
            var retVal = val/(totalProjects + 1); 
            return retVal;
        }
        /// <summary>
        /// Obtains all job offers for the given employer.
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns>List of job offers.</returns>
        public AllJobOffersByEmployer GetJobOffersByEmployer(string employerId)
        {
            using (var context = new MyLearnContext())
            {
                var jobOfferRepo = new JobOfferRepository(context);
                AllJobOffersByEmployer jobOffers = null;
                var activejobOffers = jobOfferRepo.GetEmployerActiveJobOffers(new Guid(employerId));
                var inactivejobOffers = jobOfferRepo.GetEmployerInactiveJobOffers(new Guid(employerId));
                if (activejobOffers != null && inactivejobOffers != null)
                {
                   var activejobOffersList = mapper.ActiveJobListMap(activejobOffers);
                   var finishedjobOffersList = mapper.FinishedJobListMap(inactivejobOffers);
                    jobOffers = new AllJobOffersByEmployer
                    {
                        ActiveJobOffers = activejobOffersList,
                        FinishedJobOffers = finishedjobOffersList
                    };
                }
                jobOfferRepo.Dispose();
                return jobOffers;
            }
        }
        /// <summary>
        /// Get specific job offer from given id.
        /// </summary>
        /// <param name="jobOfferId"></param>
        /// <returns>Specific job offer.</returns>
        public JobOffer GetJobOffer(string jobOfferId)
        {
            using (var context = new MyLearnContext())
            {
                var jobOfferRepo = new JobOfferRepository(context);
                var joboffer = jobOfferRepo.GetJobOfferById(Guid.Parse(jobOfferId));
                var resultOffer = new JobOffer();
                if (joboffer != null)
                {
                    resultOffer.JobOfferTitle = joboffer.Name;
                    resultOffer.JobOfferId = joboffer.JobOfferId.ToString();
                    resultOffer.Technologies = mapper.TechnologiesToString(joboffer.Technologies);
                    resultOffer.StartDate = joboffer.StartDate.ToString();
                    resultOffer.EndDate = joboffer.EndDate.ToString();
                    resultOffer.Description = joboffer.Description;
                    resultOffer.Budget = joboffer.Budget;
                    resultOffer.State = joboffer.IsActive;
                    resultOffer.EmployerName = joboffer.Employer.CompanyName;
                    resultOffer.EmployerUserId = joboffer.EmployerId.ToString();
                    resultOffer.StateDescription = joboffer.StateDescription;
                }
                jobOfferRepo.Dispose();
                return resultOffer;
            }
        }
        /// <summary>
        /// Obtains a list of job offers by a given technology.
        /// </summary>
        /// <param name="technology"></param>
        /// <returns></returns>
        public List<JobOffer> GetJobOffersByTechnology(string technology)
        {
            using (var context = new MyLearnContext())
            {
                var technologyRepo = new TechnologyRepository(context);
                var jobOfferRepo = new JobOfferRepository(context);
                var techList = technologyRepo.GetTechnologiesByName(technology);
                var resJob = new List<Models.JobOffer>();
                foreach (var tech in techList)
                {
                    var jobOffersByTechnology = jobOfferRepo.GetJobOfferByTechnology(tech.TechnologyId);
                    resJob.AddRange(mapper.JobOfferMap(jobOffersByTechnology));
                    
                }
                technologyRepo.Dispose();
                jobOfferRepo.Dispose();
                return resJob;
            }
        }
        /// <summary>
        /// Obtains all job offers by title.
        /// </summary>
        /// <param name="jobOfferTitle"></param>
        /// <returns>List of job offers that match with given title.</returns>
        public List<JobOffer> GetJobOffersByName(string jobOfferTitle)
        {
            using (var context = new MyLearnContext())
            {
                var jobOfferRepo = new JobOfferRepository(context);
                var jobOffersByName = jobOfferRepo.GetJobOfferByName(jobOfferTitle);
        
                
                var resJob = mapper.JobOfferMap(jobOffersByName);
                jobOfferRepo.Dispose();
                return resJob;
            }
        }
        /// <summary>
        /// Obtains bids by job offer.
        /// </summary>
        /// <param name="jobOfferId"></param>
        /// <returns></returns>
        public List<JobOfferBid> GetBidsById(string jobOfferId)
        {
            using (var context = new MyLearnContext())
            {
                var bidRepo = new BidRepository(context);
                var bidList = bidRepo.GetJobOfferBids(Guid.Parse(jobOfferId));
                var resultBids = mapper.JobOfferBidMap(bidList);
                bidRepo.Dispose();
                return resultBids;
            }
        }
        /// <summary>
        /// Gets all job offers a student has or has had. 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>All student's job offers.</returns>
        public AllStudentJobOffer GetJobOffersByStudent(string studentId)
        {
            using (var context = new MyLearnContext())
            {
                var jobOfferRepo = new JobOfferRepository(context);

                AllStudentJobOffer jobOffers = null;
                var activejobOffers = jobOfferRepo.GetStudentActiveJobOffers(new Guid(studentId));
                var inactivejobOffers = jobOfferRepo.GetStudentInactiveJobOffers(new Guid(studentId));
                if (activejobOffers != null && inactivejobOffers != null)
                {
                    var activejobOffersList = mapper.ActiveJobListMap(activejobOffers);
                    var finishedjobOffersList = mapper.FinishedJobListMap(inactivejobOffers);
                    jobOffers = new AllStudentJobOffer
                    {
                        ActiveJobOffers = activejobOffersList,
                        FinishedJobOffers = finishedjobOffersList
                    };
                }
                jobOfferRepo.Dispose();
                return jobOffers;
            }
        }
    }
}
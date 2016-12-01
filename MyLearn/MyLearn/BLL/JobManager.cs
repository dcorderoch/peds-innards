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
    public class JobManager
    {
        private ModelMapper mapper;

        public JobManager()
        {
        mapper= new ModelMapper();    
        }

        public ReturnCode CreateJobOffer(NewJobOffer newOffer)
        {
            using (var context = new MyLearnContext())
            {
                var technologyManager = new TechnologyManager();
                var jobOfferRepo = new JobOfferRepository(context);
                var employerRepo = new EmployerRepository(context);
                var employer = employerRepo.GetEmployerById(Guid.Parse(newOffer.EmployerId));
            
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
                jobOffer.Technologies = technologyManager.GetTechnologies(newOffer.Technologies);
                //jobOffer.Description = newOffer.Description;
                try
                {
                    jobOfferRepo.Add(jobOffer);
                    employer.JobOffers.Add(jobOffer);
                    jobOfferRepo.SaveChanges();
                    employerRepo.SaveChanges();
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
                employerRepo.Dispose();
                return returnCode;
            }    
        }

        public ReturnCode Assign(AssignJobOffer jobOffer)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode success = new ReturnCode();
              
                var jobOfferRepo = new JobOfferRepository(context);
                var studentRepo= new StudentRepository(context);
                var bidRepo = new BidRepository(context);

                var student = studentRepo.GetStudentById(Guid.Parse(jobOffer.JobOfferId));
                var currJobOffer = jobOfferRepo.GetJobOfferById(Guid.Parse(jobOffer.JobOfferId));
                var bids = bidRepo.GetJobOfferBids(Guid.Parse(jobOffer.JobOfferId));

                if (student != null && currJobOffer != null && bids != null)
                {
                    var notificationManager = new NotificationManager();

                    currJobOffer.IsActive = 1;
                    currJobOffer.UserId = Guid.Parse(jobOffer.StudentId);
                    jobOfferRepo.SaveChanges();

                    student.JobOffers.Add(jobOfferRepo.GetJobOfferById(Guid.Parse(jobOffer.JobOfferId)));
                    student.Notifications.Add(notificationManager.CreateNotification(jobOffer.StudentId,
                        currJobOffer.Name));
                    studentRepo.SaveChanges();

                    bids.Clear();
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
                return success;
            }
        }

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
                        }
                        else //Fracasó
                        {
                            currentJobOffer.IsActive = closeJobOffer.State;
                            currentJobOffer.StateDescription = closeJobOffer.StateDescription;
                            currentJobOffer.Score = closeJobOffer.Stars; //asignar nota del trabajo
                            student.AvgProjects = CalculateAverage(ObtainGrade(closeJobOffer.Stars),
                                    student.NumSuceedProjects + student.NumFailedProjects, student.AvgProjects); //actualizar promedio de estudiante
                            student.NumFailedProjects += 1; //aumentar numero de proyectos fracasados
                        }
                    }
                    else //No fue asignada al estudiante
                    {
                        currentJobOffer.IsActive = closeJobOffer.State;
                        currentJobOffer.StateDescription = closeJobOffer.StateDescription;
                        currentJobOffer.Score = 0;
                    }
                    retVal.ReturnStatus = 1;
                }
                catch (Exception)
                {
                    retVal.ReturnStatus = 0; ;
                }
                return retVal;
            }
        }

        private int ObtainGrade(int stars)
        {
            return 20*stars;
        }

        private decimal CalculateAverage(int grade, int totalProjects, decimal currentAverage)
        {
            var val = currentAverage*totalProjects + grade;
            var retVal = val/(totalProjects + 1); 
            return retVal;
        }

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
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class TechnologyRepository : Repository<Technology>
    {
        public List<Technology> GetStudentTechnologies(Guid UserId)
        {
            return DbSet.Where(t => t.StudentTechnologies.Any(s => s.UserId.Equals(UserId))).ToList();
        }

        public List<Technology> GetProjectTechnologies(Guid ProjectId)
        {
            return DbSet.Where(t => t.ProjectTechnologies.Any(p => p.ProjectId.Equals(ProjectId))).ToList();
        }

        public List<Technology> GetJobOfferTechnologies(Guid JobOfferId)
        {
            return DbSet.Where(t => t.JobOfferTechnologies.Any(j => j.JobOfferId.Equals(JobOfferId))).ToList();
        }

        public List<Technology> GetCourseYTechnologies(Guid CourseId)
        {
            return DbSet.Where(t => t.CourseTechnologies.Any(c => c.CourseId.Equals(CourseId))).ToList();
        }
    }
}

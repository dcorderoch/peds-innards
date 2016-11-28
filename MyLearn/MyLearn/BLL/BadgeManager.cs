﻿using System;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using System.Collections.Generic;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Badge = MyLearn.Models.Badge;

namespace MyLearn.BLL
{
    public class BadgeManager
    {
        public ReturnCode GiveBadge(NewBadge newBadge)
        {
            var retVal = new ReturnCode();
            // SUBJECT TO CHANGE
            return retVal;
        }
        public List<Models.Badge> GetAll(SharedAreaCredentials credentials)
        {
            ProjectRepository projectRepo = new ProjectRepository();
            BadgeRepository badgeRepo = new BadgeRepository();
            Project project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
            Guid projectId = new Guid();
            if (project != null)
            {
                projectId = project.ProjectId;
            }
            List<MyLearnDAL.Models.Badge> retval = badgeRepo.GetProjectBadges(projectId);
            List<Badge> badges = new List<Badge>();
            foreach (MyLearnDAL.Models.Badge badge in retval)
            {
                Badge newBadge = new Badge();
                newBadge.BadgeId = badge.BadgeId.ToString();
                newBadge.BadgeDescription = badge.Achievement.Description;
                badges.Add(newBadge);
            }
            return badges;
        }
        public ReturnCode Brag(BadgeIdentifier badgeId)
        {
            var retVal = new ReturnCode();
            retVal.ReturnStatus = 0;
            // se hace el tweet ANTES de hacer lo demás, si el método retorna FALSE
            // se retorna que falló la vara
            var tweeter = new Tweeter();
            if(tweeter.tweet("mensaje"))
            {
                retVal.ReturnStatus += 1;
                // marcar la maire como que ya se hizo alarde
            }
            // SUBJECT TO CHANGE
            return retVal;
        }
    }
}
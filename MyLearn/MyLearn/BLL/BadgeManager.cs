using System;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using System.Collections.Generic;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Badge = MyLearnDAL.Models.Badge;

namespace MyLearn.BLL
{
    public class BadgeManager
    {
        public ReturnCode GiveBadge(NewBadge newBadge)
        {
            using (var context = new MyLearnContext())
            {
                BadgeRepository badgeRepo = new BadgeRepository(context);
                ProjectRepository projectRepo = new ProjectRepository(context);
                Badge badge = new Badge();
                Project project = new Project();
                var retVal = new ReturnCode();

                badge.BadgeId = Guid.NewGuid();
                badge.Bragged = 0;
                badge.AchievementId = new Guid(newBadge.AchievementId);
                project = projectRepo.GetProjectByStudentAndCourseId(new Guid(newBadge.StudentUserId), new Guid(newBadge.CourseId));
                badge.ProjectId = project.ProjectId;
                if (badge.AchievementId != null && badge.ProjectId != null)
                {
                    badgeRepo.Add(badge);
                    retVal.ReturnStatus = 1;
                }
                badgeRepo.Dispose();
                projectRepo.Dispose();
                return retVal;
            }
        }

        public List<Models.Badge> GetAll(SharedAreaCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                ProjectRepository projectRepo = new ProjectRepository(context);
                BadgeRepository badgeRepo = new BadgeRepository(context);
                Project project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
                Guid projectId = new Guid();
                if (project != null)
                {
                    projectId = project.ProjectId;
                }
                List<MyLearnDAL.Models.Badge> retval = badgeRepo.GetProjectBadges(projectId);
                List<Models.Badge> badges = new List<Models.Badge>();
                foreach (MyLearnDAL.Models.Badge badge in retval)
                {
                    Models.Badge newBadge = new Models.Badge();
                    newBadge.BadgeId = badge.AchievementId.ToString();
                    newBadge.BadgeDescription = badge.Achievement.Description;
                    newBadge.Value = badge.Achievement.Score;
                    newBadge.Alardeado = badge.Bragged;
                    newBadge.Awarded = 1;
                    badges.Add(newBadge);
                }
                projectRepo.Dispose();
                badgeRepo.Dispose();
                return badges;
            }
        }

        public ReturnCode Brag(BadgeIdentifier badgeId)
        {
            using (var context = new MyLearnContext())
            {
                BadgeRepository badgeRepo = new BadgeRepository(context);
                Badge badge = new Badge();
                badge = badgeRepo.GetBadgeById(new Guid(badgeId.BadgeId));
                var retVal = new ReturnCode();
                retVal.ReturnStatus = 0;

                // se hace el tweet ANTES de hacer lo demás, si el método retorna FALSE
                // se retorna que falló la vara
                var tweeter = new Tweeter();
                if (tweeter.tweet(badgeId.StudentName + " " + badgeId.StudentLastName + " obtuvo una medalla."))
                {
                    retVal.ReturnStatus += 1;
                    badge.Bragged = 1;
                }
                badgeRepo.Dispose();
                return retVal;
            }
        }
    }
}
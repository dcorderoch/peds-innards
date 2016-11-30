using System;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearn.TwitterPoster;
using System.Collections.Generic;
using MyLearn.Utils;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Badge = MyLearnDAL.Models.Badge;

namespace MyLearn.BLL
{
    public class BadgeManager
    {
        private ModelMapper mapper = new ModelMapper();
        public ReturnCode GiveBadge(NewBadge newBadge)
        {
            using (var context = new MyLearnContext())
            {
                BadgeRepository badgeRepo = new BadgeRepository(context);
                ProjectRepository projectRepo = new ProjectRepository(context);
                Badge badge = new Badge();
                var retVal = new ReturnCode();
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(newBadge.StudentUserId), new Guid(newBadge.CourseId));
                badge.BadgeId = Guid.NewGuid();
                badge.Bragged = 0;
                badge.AchievementId = new Guid(newBadge.AchievementId);
                badge.ProjectId = project.ProjectId;
                if (badge.AchievementId != null && badge.ProjectId != null)
                {
                    project.Badges.Add(badge);
                    project.Score += badge.Achievement.Score;
                    badgeRepo.Add(badge);
                    badgeRepo.SaveChanges();
                    projectRepo.SaveChanges();
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
                List<Models.Badge> badges = new List<Models.Badge>();
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
                var retval = badgeRepo.GetProjectBadges(project.ProjectId);
                if (retval!= null)
                {
                    badges = mapper.BadgeListMap(retval);
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
                var badge = badgeRepo.GetBadgeById(new Guid(badgeId.BadgeId));
                var retVal = new ReturnCode();
                retVal.ReturnStatus = 0;
                
                var tweeter = new Tweeter();
                if (tweeter.tweet(badgeId.StudentName + " " + badgeId.StudentLastName + " obtuvo una medalla."))
                {
                    retVal.ReturnStatus += 1;
                    badge.Bragged = 1;
                    badgeRepo.SaveChanges();
                }
                badgeRepo.Dispose();
                return retVal;
            }
        }
    }
}
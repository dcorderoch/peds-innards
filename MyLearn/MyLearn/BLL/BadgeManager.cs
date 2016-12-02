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
                    project.Score +=
                        project.Course.Achievements.Find(a => a.AchievementId.Equals(badge.AchievementId))
                            .Score;
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
                var retVal = new List<Models.Badge>();

                ProjectRepository projectRepo = new ProjectRepository(context);
                BadgeRepository badgeRepo = new BadgeRepository(context);
                AchievementRepository achievementRepo = new AchievementRepository(context);
                
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));

                var badges = (project != null)?badgeRepo.GetProjectBadges(project.ProjectId):new List<Badge>();
                var achievements = achievementRepo.GetCourseAchievements(Guid.Parse(credentials.CourseId));
                var projectbadges = project.Badges;

                foreach(var achievement in achievements)
                {
                    var achId = achievement.AchievementId.ToString();
                    retVal.Add(new Models.Badge
                    {
                        BadgeDescription = achievement.Description,
                        BadgeId = achievement.AchievementId.ToString(),
                        Value = achievement.Score,
                        Awarded = (badges.Find(b => b.AchievementId.ToString().Equals(achId)) != null)?1:0,
                        Alardeado = (badges.Find(b => b.AchievementId.ToString().Equals(achId)) != null)? badges.Find(b => b.AchievementId.ToString().Equals(achId)).Bragged : 0
                    });
                }

                achievementRepo.Dispose();
                projectRepo.Dispose();
                badgeRepo.Dispose();

                return retVal;
            }
        }

        public ReturnCode Brag(BadgeIdentifier badgeId)
        {
            using (var context = new MyLearnContext())
            {
                var badgeRepo = new BadgeRepository(context);
                var achievementRepo = new AchievementRepository(context);
                var achievement = achievementRepo.GetAchievementById(new Guid(badgeId.BadgeId));
                var badge = badgeRepo.GetBadgeByAchievementId(achievement.AchievementId);
                var retVal = new ReturnCode();
                retVal.ReturnStatus = 0;
                
                var tweeter = new Tweeter();
                if (tweeter.tweet(badgeId.StudentName + " " + badgeId.StudentLastName + " obtuvo una medalla!!"))
                {
                    retVal.ReturnStatus += 1;
                    badge.Bragged = 1;
                    badgeRepo.SaveChanges();
                }
                achievementRepo.Dispose();
                badgeRepo.Dispose();
                return retVal;
            }
        }
    }
}
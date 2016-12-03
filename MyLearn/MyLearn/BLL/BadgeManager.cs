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
        /// <summary>
        /// Class created with the intention of handling the use of badges in MyLearn platform.
        /// </summary>
        private ModelMapper mapper = new ModelMapper();

        /// <summary>
        /// Gives a badge according to the input information to a certain project.
        /// </summary>
        /// <param name="newBadge"></param>
        /// <returns>Return code indicating whether it was successful or not.</returns>
        public ReturnCode GiveBadge(NewBadge newBadge)
        {
            using (var context = new MyLearnContext())
            {
                BadgeRepository badgeRepo = new BadgeRepository(context);
                ProjectRepository projectRepo = new ProjectRepository(context);
                var achievementRepo = new AchievementRepository(context);
                var achievement = achievementRepo.GetAchievementById(Guid.Parse(newBadge.AchievementId));
                Badge badge = new Badge();
                var retVal = new ReturnCode();
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(newBadge.StudentUserId), new Guid(newBadge.CourseId));
                badge.BadgeId = Guid.NewGuid();
                badge.Bragged = 0;
                badge.AchievementId = new Guid(newBadge.AchievementId);
                badge.ProjectId = project.ProjectId;
                if (badge.AchievementId != null && badge.ProjectId != null)
                {
                    if (project.Score + achievement.Score <= 100)
                    {
                        project.Badges.Add(badge);
                        project.Score += achievement.Score;
                        badgeRepo.Add(badge);
                        badgeRepo.SaveChanges();
                        projectRepo.SaveChanges();
                        retVal.ReturnStatus = 1;
                    }
                    else
                    {
                        retVal.ReturnStatus = 0;
                    }
                }
                badgeRepo.Dispose();
                projectRepo.Dispose();
                achievementRepo.Dispose();
                return retVal;
            }
        }

        /// <summary>
        /// Obtains a list of all badges awarded to a student in a specific course.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>List of badges.</returns>
        public List<Models.Badge> GetAll(SharedAreaCredentials credentials)
        {
            using (var context = new MyLearnContext())
            {
                var retVal = new List<Models.Badge>();

                ProjectRepository projectRepo = new ProjectRepository(context);
                BadgeRepository badgeRepo = new BadgeRepository(context);
                AchievementRepository achievementRepo = new AchievementRepository(context);
                
                var project = projectRepo.GetProjectByStudentAndCourseId(new Guid(credentials.StudentUserId), new Guid(credentials.CourseId));
                List<Badge> badges = null;

                if(project != null) {
                    var projectbadges = project.Badges;
                    badges = badgeRepo.GetProjectBadges(project.ProjectId);
                } else {
                    badges = new List<Badge>();
                }

                var achievements = achievementRepo.GetCourseAchievements(Guid.Parse(credentials.CourseId));

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

        /// <summary>
        /// Method in charge of posting a twit in Twitter after a badge has been awarded.
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns>Return code indicating if operation was successful.</returns>
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
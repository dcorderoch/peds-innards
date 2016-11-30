using System;
using System.Collections.Generic;
using System.Linq;  
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class AchievementRepository : Repository<Achievement>
    {
        public AchievementRepository(MyLearnContext context) : base(context) { }

        public List<Achievement> GetCourseAchievements(Guid CourseId)
        {
            return DbSet.Where(a => a.Course.CourseId.Equals(CourseId)).ToList();
        }

        public Achievement GetAchievementById(Guid achievementId)
        {
            return DbSet.Find(achievementId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class BadgeRepository : Repository<Badge>
    {
        public BadgeRepository(MyLearnContext context) : base(context) { }

        public List<Badge> GetProjectBadges(Guid ProjectId)
        {
            return DbSet.Where(b => b.Project.ProjectId.Equals(ProjectId)).ToList();
        }

        public Badge GetBadgeById(Guid badgeId)
        {
            return DbSet.Find(badgeId);
        }

        public Badge GetBadgeByAchievementId(Guid achievementId)
        {
            return DbSet.FirstOrDefault(b => b.AchievementId.Equals(achievementId));
        }
    }
}

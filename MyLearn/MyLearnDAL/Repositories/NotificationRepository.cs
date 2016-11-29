using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class NotificationRepository : Repository<Notification>
    {
        public List<Notification> GetNotifications(Guid userId)
        {
            return DbSet.Where(n => n.Student.UserId.Equals(userId)).ToList();
        }

        public Notification GetNotificacionById(Guid notificationId)
        {
            return DbSet.Find(notificationId);
        }
    }
}

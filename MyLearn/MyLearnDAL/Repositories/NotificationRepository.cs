using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Notification repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class NotificationRepository : Repository<Notification>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public NotificationRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get notifications for the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A list of notifications</returns>
        public List<Notification> GetNotifications(Guid userId)
        {
            return DbSet.Where(n => n.Student.UserId.Equals(userId)).ToList();
        }

        /// <summary>
        /// Get notification by its id
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns>A notification</returns>
        public Notification GetNotificacionById(Guid notificationId)
        {
            return DbSet.Find(notificationId);
        }
    }
}

using System;
using System.Collections.Generic;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class built in order to handle a student's notifications.
    /// </summary>
    public class NotificationManager
    {
        /// <summary>
        /// Gets all notifications for the given student.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>List of notifications for given students.</returns>
        public List<string> GetStudentNotifications(string studentId)
        {
            List<string> notificationList = new List<string>();
            using (var context = new MyLearnContext())
            {
                try
                {
                    var notificationRepository = new NotificationRepository(context);
                    var notifications = notificationRepository.GetNotifications(Guid.Parse(studentId));
                    foreach (var notification in notifications)
                    {
                        notificationList.Add(notification.Message);
                    }
                }
                catch (Exception)
                {

                }
            }
            
            return notificationList;

        }
        /// <summary>
        /// Creates a new notification for a student and given job offer.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="jobOfferName"></param>
        /// <returns>Notification indicating that a student won a job offer.</returns>
        public Notification CreateNotification(string studentId, string jobOfferName)
        {
            var newNotification = new Notification
            {
                NotificationId = Guid.NewGuid(),
                State = 0,
                UserId = Guid.Parse(studentId),
                Message = "Has ganado la oferta de trabajo: " + jobOfferName + "."
            };
            return newNotification;
        }
    
    }
}
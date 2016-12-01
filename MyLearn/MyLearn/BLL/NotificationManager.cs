using System;
using System.Collections.Generic;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class NotificationManager
    {
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
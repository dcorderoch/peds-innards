using System;
using System.Collections.Generic;
using MyLearnDAL.Models;

namespace MyLearn.BLL
{
    public class NotificationManager
    {
        public List<string> GetStudentNotifications(string studentId)
        {
            List<string> notificationList = new List<string>();
            //Retrieve notifications from DB
            //subject to change


            return notificationList;

        }

        public Notification CreateNotification(string studentId, string jobOfferName)
        {
            var newNotification = new Notification();
            newNotification.NotificationId = Guid.NewGuid();
            newNotification.State = 0;
            newNotification.UserId = Guid.Parse(studentId);
            newNotification.Message = "Has ganado la oferta de trabajo: " + jobOfferName + ".";
            return newNotification;
        }
    
    }
}
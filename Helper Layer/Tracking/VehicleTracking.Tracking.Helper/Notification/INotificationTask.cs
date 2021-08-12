using System.Collections.Generic;
using VehicleTracking.Tracking.Helper.Models;
using static VehicleTracking.Tracking.Helper.Models.NotificationModel;
using FluentValidation.Results;

namespace VehicleTracking.Tracking.Helper.Notification
{
    public interface INotificationTask
    {
        IReadOnlyCollection<NotificationModel> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
        void AddNotification(string key, string message);
        void AddNotifications(IReadOnlyCollection<NotificationModel> notifications);
        void AddNotifications(IList<NotificationModel> notifications);
        void AddNotifications(ICollection<NotificationModel> notifications);
        void AddNotifications(ValidationResult validationResult);
    }

}

using FluentValidation.Results;
using System.Collections.Generic;
using VehicleTracking.Vehicle.Helper.Models;
using static VehicleTracking.Vehicle.Helper.Models.NotificationModel;

namespace VehicleTracking.Vehicle.Helper.Notification
{
    public interface INotificationVehicleTask
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

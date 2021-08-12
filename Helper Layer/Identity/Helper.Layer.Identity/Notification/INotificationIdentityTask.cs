using FluentValidation.Results;
using Helper.Layer.Identity.Models;
using System.Collections.Generic;
using static Helper.Layer.Identity.Models.NotificationModel;

namespace Helper.Layer.Identity.Notification
{
    public interface INotificationIdentityTask
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

using FluentValidation.Results;
using Helper.Layer.Identity.Models;
using System.Collections.Generic;
using System.Linq;
using static Helper.Layer.Identity.Models.NotificationModel;

namespace Helper.Layer.Identity.Notification
{

    public class NotificationIdentityTask : INotificationIdentityTask
    {
        private readonly List<NotificationModel> _notifications;

        public IReadOnlyCollection<NotificationModel> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public NotificationIdentityTask()
        {
            _notifications = new List<NotificationModel>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationModel(key, message));
        }

        public void AddNotification(string key, string message, ENotificationType notificationType)
        {
            _notifications.Add(new NotificationModel(key, message, notificationType));
        }

        public void AddNotifications(IReadOnlyCollection<NotificationModel> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(IList<NotificationModel> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ICollection<NotificationModel> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.PropertyName, error.ErrorMessage);
            }
        }

    }

}

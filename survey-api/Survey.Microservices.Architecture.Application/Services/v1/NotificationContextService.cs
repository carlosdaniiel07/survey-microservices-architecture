using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.Models.v1;

namespace Survey.Microservices.Architecture.Application.Services.v1
{
    public class NotificationContextService : INotificationContextService
    {
        private readonly List<Notification> _notifications;

        public NotificationContextService()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(string code) =>
            _notifications.Add(new Notification(code));

        public void AddNotification(Notification notification) =>
            _notifications.Add(notification);
    }
}

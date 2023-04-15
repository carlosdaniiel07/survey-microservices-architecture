using Survey.Microservices.Architecture.Domain.Models.v1;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Services.v1
{
    public interface INotificationContextService
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        void AddNotification(string code);
        void AddNotification(Notification notification);
    }
}

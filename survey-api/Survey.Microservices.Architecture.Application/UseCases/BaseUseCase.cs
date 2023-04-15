using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.Models.v1;

namespace  Survey.Microservices.Architecture.Application.UseCases
{
    public abstract class BaseUseCase<TUseCase>
    {
        protected readonly ILogger<TUseCase> _logger;
        protected readonly INotificationContextService _notificationContextService;

        protected BaseUseCase(ILogger<TUseCase> logger, INotificationContextService notificationContextService)
        {
            _logger = logger;
            _notificationContextService = notificationContextService;
        }

        protected void AddNotification(string code) =>
            _notificationContextService.AddNotification(code);

        protected void AddNotification(Notification notification) =>
            _notificationContextService.AddNotification(notification);
    }
}

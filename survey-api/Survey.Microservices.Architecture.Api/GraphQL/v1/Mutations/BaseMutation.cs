using Survey.Microservices.Architecture.Domain.Exceptions.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases;

namespace Survey.Microservices.Architecture.Api.GraphQL.v1.Mutations
{
    public class BaseMutation
    {
        protected async Task<TResponse> ExecuteAsync<TResponse, TUseCase, TRequest>(
            INotificationContextService notificationContextService,
            TUseCase useCase,
            TRequest request
        ) where TUseCase : IBaseUseCase<TRequest, TResponse>
        {
            var response = await useCase.ExecuteAsync(request);
            var hasNotifications = notificationContextService.Notifications.Any();

            if (hasNotifications)
                throw new BusinessRuleException(notificationContextService.Notifications.First().Code);

            return response;
        }
    }
}

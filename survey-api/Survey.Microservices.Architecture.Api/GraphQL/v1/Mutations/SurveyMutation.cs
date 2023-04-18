using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;

namespace Survey.Microservices.Architecture.Api.GraphQL.v1.Mutations
{
    public class SurveyMutation : BaseMutation
    {
        public async Task<AddSurveyAnswerResponse> AddSurveyAnswer(
            [Service] IAddSurveyAnswerUseCase useCase,
            [Service] INotificationContextService notificationContextService,
            AddSurveyAnswerRequest input
        ) =>
            await ExecuteAsync<AddSurveyAnswerResponse, IAddSurveyAnswerUseCase, AddSurveyAnswerRequest>(notificationContextService, useCase, input);
    }
}

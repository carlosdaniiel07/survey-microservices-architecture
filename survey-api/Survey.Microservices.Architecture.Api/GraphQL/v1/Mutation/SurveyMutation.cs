using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;

namespace Survey.Microservices.Architecture.Api.GraphQL.v1.Mutation
{
    public class SurveyMutation
    {
        public async Task<AddSurveyAnswerResponse> AddSurveyAnswer(AddSurveyAnswerRequest input, [Service] IAddSurveyAnswerUseCase useCase) =>
            await useCase.ExecuteAsync(input);
    }
}

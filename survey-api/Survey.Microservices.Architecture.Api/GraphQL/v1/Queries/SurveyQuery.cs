using HotChocolate.Authorization;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults;

namespace Survey.Microservices.Architecture.Api.GraphQL.v1.Queries
{
    public class SurveyQuery
    {
        [Authorize]
        public async Task<GetSurveyResultsResponse> GetSurveyResults(GetSurveyResultsRequest input, [Service] IGetSurveyResultsUseCase useCase) =>
            await useCase.ExecuteAsync(input);
    }
}

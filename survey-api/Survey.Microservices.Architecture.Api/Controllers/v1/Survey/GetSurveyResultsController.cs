using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Survey
{
    [Route("api/v1/surveys")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Survey")]
    public class GetSurveyResultsController : ControllerBase
    {
        private readonly IGetSurveyResultsUseCase _getSurveyResultsUseCase;

        public GetSurveyResultsController(IGetSurveyResultsUseCase getSurveyResultsUseCase)
        {
            _getSurveyResultsUseCase = getSurveyResultsUseCase;
        }

        [HttpGet("{surveyId}/results")]
        [ProducesResponseType(typeof(GetSurveyResultsResponse), 200)]
        public async Task<IActionResult> Get(Guid surveyId) =>
            Ok(await _getSurveyResultsUseCase.ExecuteAsync(new GetSurveyResultsRequest(surveyId)));
    }
}

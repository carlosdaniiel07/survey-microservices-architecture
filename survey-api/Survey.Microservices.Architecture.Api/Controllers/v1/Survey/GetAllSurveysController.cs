using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetAllSurveys;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Survey
{
    [Route("api/v1/surveys")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Survey")]
    public class GetAllSurveysController : ControllerBase
    {
        private readonly IGetAllSurveysUseCase _getAllSurveysUseCase;

        public GetAllSurveysController(IGetAllSurveysUseCase getAllSurveysUseCase)
        {
            _getAllSurveysUseCase = getAllSurveysUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetSurveyResultsResponse), 200)]
        public async Task<IActionResult> Get() =>
            Ok(await _getAllSurveysUseCase.ExecuteAsync(new GetAllSurveysRequest()));
    }
}

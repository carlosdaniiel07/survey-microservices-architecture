using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Survey
{
    [Route("api/v1/surveys")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Survey")]
    public class CreateSurveyController : ControllerBase
    {
        private readonly ICreateSurveyUseCase _createSurveyUseCase;

        public CreateSurveyController(ICreateSurveyUseCase createSurveyUseCase)
        {
            _createSurveyUseCase = createSurveyUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateSurveyResponse), 200)]
        public async Task<IActionResult> Post([FromBody] CreateSurveyRequest createSurveyRequest) =>
            Ok(await _createSurveyUseCase.ExecuteAsync(createSurveyRequest));
    }
}

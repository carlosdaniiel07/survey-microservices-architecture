using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Survey
{
    [Route("api/v1/surveys")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Survey")]
    public class AddSurveyAnswerController : ControllerBase
    {
        private readonly IAddSurveyAnswerUseCase _addSurveyAnswerUseCase;

        public AddSurveyAnswerController(IAddSurveyAnswerUseCase addSurveyAnswerUseCase)
        {
            _addSurveyAnswerUseCase = addSurveyAnswerUseCase;
        }

        [HttpPost("{surveyId}/answer")]
        [ProducesResponseType(typeof(AddSurveyAnswerResponse), 200)]
        public async Task<IActionResult> Post(Guid surveyId, [FromBody] AddSurveyAnswerRequest addSurveyAnswerRequest)
        {
            addSurveyAnswerRequest.SurveyId = surveyId;
            return Ok(await _addSurveyAnswerUseCase.ExecuteAsync(addSurveyAnswerRequest));
        }
    }
}

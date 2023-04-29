using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Exceptions;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.AddSurveyAnswer
{
    public class AddSurveyAnswerUseCase : BaseUseCase<AddSurveyAnswerUseCase>, IAddSurveyAnswerUseCase
    {
        private readonly ISurveyService _surveyService;
        private readonly IAnswerRepository _answerRepository;

        public AddSurveyAnswerUseCase(
            ILogger<AddSurveyAnswerUseCase> logger,
            INotificationContextService notificationContextService,
            ISurveyService surveyService,
            IAnswerRepository answerRepository
        ) : base(logger, notificationContextService)
        {
            _surveyService = surveyService;
            _answerRepository = answerRepository;
        }

        public async Task<AddSurveyAnswerResponse> ExecuteAsync(AddSurveyAnswerRequest request)
        {
            try
            {
                _logger.LogInformation($"Adding a new answer to survey {request.SurveyId}");

                var survey = await _surveyService.GetByIdAsync(request.SurveyId);
                var isValidAnswer = survey.AvailableAnswers.Contains(request.Value);

                if (!isValidAnswer)
                {
                    AddNotification("INVALID_SURVEY_ANSWER");
                    return default;
                }

                var answer = await _answerRepository.AddAsync(new Answer
                {
                    Value = request.Value,
                    SurveyId = survey.Id,
                });

                return new AddSurveyAnswerResponse
                {
                    Id = answer.Id,
                };
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex, $"Error while adding a new answer to survey {request.SurveyId}");
                AddNotification(ex.Message);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding a new answer to survey {request.SurveyId}");
                throw;
            }
        }
    }
}

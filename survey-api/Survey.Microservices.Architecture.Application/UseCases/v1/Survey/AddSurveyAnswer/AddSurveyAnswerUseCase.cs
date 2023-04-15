using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Exceptions;
using Survey.Microservices.Architecture.Domain.Exceptions.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.AddSurveyAnswer
{
    public class AddSurveyAnswerUseCase : BaseUseCase<AddSurveyAnswerUseCase>, IAddSurveyAnswerUseCase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ICacheService _cacheService;

        public AddSurveyAnswerUseCase(
            ILogger<AddSurveyAnswerUseCase> logger,
            INotificationContextService notificationContextService,
            ISurveyRepository surveyRepository,
            IAnswerRepository answerRepository,
            ICacheService cacheService
        ) : base(logger, notificationContextService)
        {
            _surveyRepository = surveyRepository;
            _answerRepository = answerRepository;
            _cacheService = cacheService;
        }

        public async Task<AddSurveyAnswerResponse> ExecuteAsync(AddSurveyAnswerRequest request)
        {
            try
            {
                var survey = await GetSurveyByIdAsync(request.SurveyId);
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

        private async Task<SurveyEntity> GetSurveyByIdAsync(Guid surveyId)
        {
            var cacheKey = $"surve:{surveyId}";
            var surveyFromCache = await _cacheService.RetrieveAsync<SurveyEntity>(cacheKey);

            if (surveyFromCache != null)
                return surveyFromCache;

            var survey = await _surveyRepository.GetByIdAsync(surveyId);

            if (survey == null)
                throw new SurveyNotFoundException();

            if (!survey.IsActive)
                throw new SurveyNotActiveException();

            await _cacheService.AddAsync(cacheKey, survey, TimeSpan.FromMinutes(3));

            return survey;
        }
    }
}

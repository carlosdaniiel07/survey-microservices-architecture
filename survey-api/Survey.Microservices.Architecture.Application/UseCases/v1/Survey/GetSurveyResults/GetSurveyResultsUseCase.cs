using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.GetSurveyResults
{
    public class GetSurveyResultsUseCase : BaseUseCase<GetSurveyResultsUseCase>, IGetSurveyResultsUseCase
    {
        private readonly ISurveyService _surveyService;
        private readonly ICacheService _cacheService;
        private readonly IAnswerRepository _answerRepository;

        public GetSurveyResultsUseCase(
            ILogger<GetSurveyResultsUseCase> logger,
            INotificationContextService notificationContextService,
            ISurveyService surveyService,
            ICacheService cacheService,
            IAnswerRepository answerRepository
        ) : base(logger, notificationContextService)
        {
            _surveyService = surveyService;
            _cacheService = cacheService;
            _answerRepository = answerRepository;
        }

        public async Task<GetSurveyResultsResponse> ExecuteAsync(GetSurveyResultsRequest request)
        {
            try
            {
                _logger.LogInformation("Retrieving results from survey {surveyId}", request.SurveyId);

                var survey = await _surveyService.GetByIdAsync(request.SurveyId);
                var results = await GetResultsAsync(request.SurveyId);

                return new GetSurveyResultsResponse
                {
                    Id = survey.Id,
                    Question = survey.Question,
                    AvailableAnswers = survey.AvailableAnswers,
                    StartAt = survey.StartAt,
                    EndAt = survey.EndAt,
                    IsActive = survey.IsActive,
                    Results = results,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving results from survey {surveyId}", request.SurveyId);
                throw;
            }
        }

        private async Task<IEnumerable<GetSurveyResultsResponse.Result>> GetResultsAsync(Guid surveyId)
        {
            var cacheKey = $"survey-results:{surveyId}";
            var resultsFromCache = await _cacheService.RetrieveAsync<IEnumerable<GetSurveyResultsResponse.Result>>(cacheKey);

            if (resultsFromCache != null)
            {
                _logger.LogInformation("Retrieved survey {surveyId} results from cache", surveyId);
                return resultsFromCache;
            }

            var answers = await _answerRepository.GetAllBySurveyIdAsync(surveyId);
            var totalAnswers = answers.Count();
            var results = answers
                .GroupBy(answer => answer.Value)
                .Select(item =>
                {
                    var count = item.Count();
                    var percentage = Math.Round((double)count / totalAnswers * 100, 2);

                    return new GetSurveyResultsResponse.Result
                    {
                        Answer = item.Key,
                        Count = count,
                        Percentage = percentage,
                    };
                });

            await _cacheService.AddAsync(cacheKey, results, TimeSpan.FromMinutes(3));

            _logger.LogInformation("Survey {surveyId} results added to cache", surveyId);

            return results;
        }
    }
}

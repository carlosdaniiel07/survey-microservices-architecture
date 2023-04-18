using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Exceptions.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Application.Services.v1
{
    public class SurveyService : ISurveyService
    {
        private readonly ILogger<SurveyService> _logger;
        private readonly ICacheService _cacheService;
        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ILogger<SurveyService> logger, ICacheService cacheService, ISurveyRepository surveyRepository)
        {
            _logger = logger;
            _cacheService = cacheService;
            _surveyRepository = surveyRepository;
        }

        public async Task<SurveyEntity> GetByIdAsync(Guid id)
        {
            var cacheKey = $"survey:{id}";
            var surveyFromCache = await _cacheService.RetrieveAsync<SurveyEntity>(cacheKey);

            if (surveyFromCache != null)
            {
                _logger.LogInformation($"Survey {id} retrieved from cache");
                return surveyFromCache;
            }

            var survey = await _surveyRepository.GetByIdAsync(id);

            if (survey == null)
                throw new SurveyNotFoundException();

            if (!survey.IsActive)
                throw new SurveyNotActiveException();

            await _cacheService.AddAsync(cacheKey, survey, TimeSpan.FromMinutes(3));

            _logger.LogInformation($"Survey {id} added to cache");

            return survey;
        }
    }
}

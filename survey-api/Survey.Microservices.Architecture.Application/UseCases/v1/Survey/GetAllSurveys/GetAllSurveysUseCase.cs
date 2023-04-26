using AutoMapper;
using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetAllSurveys;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.GetAllSurveys
{
    public class GetAllSurveysUseCase : BaseUseCase<GetAllSurveysUseCase>, IGetAllSurveysUseCase
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public GetAllSurveysUseCase(
            ILogger<GetAllSurveysUseCase> logger,
            INotificationContextService notificationContextService,
            ISurveyRepository surveyRepository,
            ICacheService cacheService,
            IMapper mapper
        ) : base(logger, notificationContextService)
        {
            _surveyRepository = surveyRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<GetAllSurveysResponse> ExecuteAsync(GetAllSurveysRequest request)
        {
            try
            {
                var cacheKey = "surveys";
                var surveysFromCache = await _cacheService.RetrieveAsync<GetAllSurveysResponse>(cacheKey);

                if (surveysFromCache != null)
                    return surveysFromCache;

                var surveys = await _surveyRepository.GetAllAsync();
                var surveysResponse = new GetAllSurveysResponse
                {
                    Surveys = _mapper.Map<IEnumerable<GetAllSurveysResponse.Survey>>(surveys),
                };

                await _cacheService.AddAsync(cacheKey, surveysResponse, TimeSpan.FromMinutes(3));

                return surveysResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all surveys");
                throw;
            }
        }
    }
}

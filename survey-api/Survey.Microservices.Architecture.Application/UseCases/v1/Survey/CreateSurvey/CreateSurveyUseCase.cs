using AutoMapper;
using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.CreateSurvey
{
    public class CreateSurveyUseCase : BaseUseCase<CreateSurveyUseCase>, ICreateSurveyUseCase
    {
        private readonly IMapper _mapper;
        private readonly ISurveyRepository _surveyRepository;

        public CreateSurveyUseCase(
            ILogger<CreateSurveyUseCase> logger,
            INotificationContextService notificationContextService,
            IMapper mapper,
            ISurveyRepository surveyRepository
        ) : base(logger, notificationContextService)
        {
            _mapper = mapper;
            _surveyRepository = surveyRepository;
        }

        public async Task<CreateSurveyResponse> ExecuteAsync(CreateSurveyRequest request)
        {
            try
            {
                _logger.LogInformation("Creating a new survey");

                var hasAvailableAnswers = request.AvailableAnswers.Any();

                if (!hasAvailableAnswers)
                {
                    AddNotification("INVALID_DATA");
                    return default;
                }

                var isOutOfRange = request.EndAt < request.StartAt;

                if (isOutOfRange)
                {
                    AddNotification("INVALID_DATA");
                    return default;
                }

                var alreadyExists = await _surveyRepository.AnyByQuestionAndActiveAsync(request.Question);

                if (alreadyExists)
                {
                    AddNotification("SURVEY_ALREADY_EXISTS");
                    return default;
                }

                var survey = await _surveyRepository.AddAsync(_mapper.Map<SurveyEntity>(request));

                _logger.LogInformation("Survey successfully created");

                return _mapper.Map<CreateSurveyResponse>(survey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new survey");
                throw;
            }
        }
    }
}

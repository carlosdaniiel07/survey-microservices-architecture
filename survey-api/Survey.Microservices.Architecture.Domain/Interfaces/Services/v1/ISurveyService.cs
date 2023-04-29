using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Services.v1
{
    public interface ISurveyService
    {
        Task<SurveyEntity> GetByIdAsync(Guid id);
    }
}

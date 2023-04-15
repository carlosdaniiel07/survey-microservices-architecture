using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1
{
    public interface ISurveyRepository : IBaseRepository<SurveyEntity>
    {
        Task<bool> AnyByQuestionAndActiveAsync(string question);
    }
}

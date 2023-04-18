using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetAllBySurveyIdAsync(Guid surveyId);
    }
}

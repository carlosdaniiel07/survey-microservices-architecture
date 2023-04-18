using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Repositories.v1
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IConfiguration configuration)
            : base(configuration, new AnswerMapping(), "Answer")
        {

        }

        public async Task<IEnumerable<Answer>> GetAllBySurveyIdAsync(Guid surveyId)
        {
            return await _collection
                .Find(Builders<Answer>.Filter.Eq(answer => answer.SurveyId, surveyId))
                .ToListAsync();
        }
    }
}

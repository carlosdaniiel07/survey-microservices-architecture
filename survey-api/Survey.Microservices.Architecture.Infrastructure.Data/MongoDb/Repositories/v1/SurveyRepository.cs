using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Repositories.v1
{
    public class SurveyRepository : BaseRepository<SurveyEntity>, ISurveyRepository
    {
        public SurveyRepository(IConfiguration configuration)
            : base(configuration, new SurveyMapping(), "Survey")
        {

        }

        public new async Task<IEnumerable<SurveyEntity>> GetAllAsync()
        {
            return await _collection
                .Find(FilterDefinition<SurveyEntity>.Empty)
                .SortByDescending(survey => survey.CreatedAt)
                .ToListAsync();

        }

        public async Task<bool> AnyByQuestionAndActiveAsync(string question)
        {
            var builder = Builders<SurveyEntity>.Filter;
            var filter = builder.And(
                builder.Eq(survey => survey.Question, question),
                builder.Eq(survey => survey.IsActive, true)
            );
            var count = await _collection.CountDocumentsAsync(filter);

            return count > 0;
        }
    }
}

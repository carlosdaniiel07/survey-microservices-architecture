using Microsoft.Extensions.Configuration;
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
    }
}

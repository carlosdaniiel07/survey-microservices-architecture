using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Repositories.v1
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
            : base(configuration, new UserMapping(), "User")
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _collection
                .Find(Builders<User>.Filter.Eq(user => user.Email, email))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AnyByEmailAsync(string email)
        {
            var count = await _collection.CountDocumentsAsync(Builders<User>.Filter.Eq(user => user.Email, email));
            return count > 0;
        }
    }
}

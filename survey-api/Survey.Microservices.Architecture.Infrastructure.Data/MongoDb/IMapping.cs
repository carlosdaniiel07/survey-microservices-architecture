using MongoDB.Bson.Serialization;
using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb
{
    public interface IMapping<TEntity> where TEntity : BaseEntity
    {
        public BsonClassMap<TEntity> RegisterMap();
    }
}

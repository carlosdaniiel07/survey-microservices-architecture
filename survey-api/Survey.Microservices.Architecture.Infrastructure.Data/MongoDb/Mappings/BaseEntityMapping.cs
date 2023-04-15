using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings
{
    public class BaseEntityMapping
    {
        public Action<BsonClassMap<BaseEntity>> GetClassMap()
        {
            return (BsonClassMap<BaseEntity> classMap) =>
            {
                var dateTimeSerializer = new DateTimeSerializer(DateTimeKind.Utc);
                var nullableDateTimeSerializer = new NullableSerializer<DateTime>().WithSerializer(dateTimeSerializer);

                classMap.MapIdMember(survey => survey.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String))
                    .SetIdGenerator(new GuidGenerator())
                    .SetElementName("id");
                classMap.MapMember(survey => survey.CreatedAt)
                    .SetSerializer(dateTimeSerializer)
                    .SetElementName("createdAt");
                classMap.MapMember(survey => survey.UpdatedAt)
                    .SetSerializer(nullableDateTimeSerializer)
                    .SetElementName("updatedAt");
                classMap.MapMember(survey => survey.DeletedAt)
                    .SetSerializer(nullableDateTimeSerializer)
                    .SetElementName("deletedAt");
            };
        }
    }
}

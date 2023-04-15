using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings
{
    public class AnswerMapping : IMapping<Answer>
    {
        public BsonClassMap<Answer> RegisterMap()
        {
            return BsonClassMap.RegisterClassMap<Answer>(classMap =>
            {
                classMap.MapMember(survey => survey.Value).SetElementName("value");
                classMap.MapMember(survey => survey.SurveyId)
                    .SetSerializer(new GuidSerializer(BsonType.String))
                    .SetElementName("surveyId");
            });
        }
    }
}

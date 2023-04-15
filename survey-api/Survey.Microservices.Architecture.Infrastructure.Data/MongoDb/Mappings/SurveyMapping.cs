using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings
{
    public class SurveyMapping : IMapping<SurveyEntity>
    {
        public BsonClassMap<SurveyEntity> RegisterMap()
        {
            return BsonClassMap.RegisterClassMap<SurveyEntity>(classMap =>
            {
                var dateTimeSerializer = new DateTimeSerializer(DateTimeKind.Utc);
                var nullableDateTimeSerializer = new NullableSerializer<DateTime>().WithSerializer(dateTimeSerializer);

                classMap.MapMember(survey => survey.Question).SetElementName("question");
                classMap.MapMember(survey => survey.AvailableAnswers).SetElementName("availableAnswers");
                classMap.MapMember(survey => survey.StartAt)
                    .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc))
                    .SetElementName("startAt");
                classMap.MapMember(survey => survey.EndAt)
                    .SetSerializer(nullableDateTimeSerializer)
                    .SetElementName("endAt");
                classMap.MapMember(survey => survey.IsActive).SetElementName("isActive");
            });
        }
    }
}

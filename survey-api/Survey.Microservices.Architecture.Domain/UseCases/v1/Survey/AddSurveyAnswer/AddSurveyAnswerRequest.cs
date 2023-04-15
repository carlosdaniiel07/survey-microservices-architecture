using System.Text.Json.Serialization;

namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer
{
    public class AddSurveyAnswerRequest
    {
        public string Value { get; set; }

        [JsonIgnore]
        public Guid SurveyId { get; set; }
    }
}

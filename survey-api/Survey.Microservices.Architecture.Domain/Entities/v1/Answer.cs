namespace Survey.Microservices.Architecture.Domain.Entities.v1
{
    public class Answer : BaseEntity
    {
        public string Value { get; set; }
        public Guid SurveyId { get; set; }
    }
}

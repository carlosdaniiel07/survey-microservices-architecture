namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey
{
    public class CreateSurveyResponse
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<string> AvailableAnswers { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsActive { get; set; }
    }
}

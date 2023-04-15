namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey
{
    public class CreateSurveyRequest
    {
        public string Question { get; set; }
        public IEnumerable<string> AvailableAnswers { get; set; } = new List<string>();
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}

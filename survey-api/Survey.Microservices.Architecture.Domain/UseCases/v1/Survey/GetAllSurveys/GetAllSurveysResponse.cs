namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetAllSurveys
{
    public class GetAllSurveysResponse
    {
        public IEnumerable<Survey> Surveys { get; set; }

        public class Survey
        {
            public Guid Id { get; set; }
            public string Question { get; set; }
            public IEnumerable<string> AvailableAnswers { get; set; }
            public DateTime StartAt { get; set; }
            public DateTime? EndAt { get; set; }
            public bool IsActive { get; set; }
        }
    }
}

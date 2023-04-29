namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults
{
    public class GetSurveyResultsResponse
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<string> AvailableAnswers { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsActive { get; set; }
        public int TotalAnswers => Results.Sum(result => result.Count);
        public IEnumerable<Result> Results { get; set; }

        public class Result
        {
            public string Answer { get; set; }
            public int Count { get; set; }
            public double Percentage { get; set; }
        }
    }
}

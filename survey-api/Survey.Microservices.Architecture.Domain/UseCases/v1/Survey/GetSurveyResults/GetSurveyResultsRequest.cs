namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults
{
    public class GetSurveyResultsRequest
    {
        public Guid SurveyId { get; private set; }

        public GetSurveyResultsRequest(Guid surveyId)
        {
            SurveyId = surveyId;
        }
    }
}

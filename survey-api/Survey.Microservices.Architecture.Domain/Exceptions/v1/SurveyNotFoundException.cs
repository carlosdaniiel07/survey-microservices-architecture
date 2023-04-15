using System.Net;

namespace Survey.Microservices.Architecture.Domain.Exceptions.v1
{
    public class SurveyNotFoundException : BaseException
    {
        public SurveyNotFoundException() : base("SURVEY_NOT_FOUND", (int)HttpStatusCode.BadRequest)
        {

        }
    }
}

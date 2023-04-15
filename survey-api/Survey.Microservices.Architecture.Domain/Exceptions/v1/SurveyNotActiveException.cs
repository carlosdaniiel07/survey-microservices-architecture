using System.Net;

namespace Survey.Microservices.Architecture.Domain.Exceptions.v1
{
    public class SurveyNotActiveException : BaseException
    {
        public SurveyNotActiveException() : base("SURVEY_NOT_ACTIVE", (int)HttpStatusCode.BadRequest)
        {

        }
    }
}

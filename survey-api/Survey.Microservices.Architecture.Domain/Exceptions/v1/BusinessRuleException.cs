using System.Net;

namespace Survey.Microservices.Architecture.Domain.Exceptions.v1
{
    public class BusinessRuleException : BaseException
    {
        public BusinessRuleException(string message) : base(message, (int) HttpStatusCode.BadRequest)
        {

        }
    }
}

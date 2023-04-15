using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace  Survey.Microservices.Architecture.Domain.Interfaces.Services.v1
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}

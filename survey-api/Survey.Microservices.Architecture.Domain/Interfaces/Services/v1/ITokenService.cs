using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Domain.Interfaces.Services.v1
{
    public interface ITokenService
    {
        (string accessToken, int expirationInMinutes) GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}

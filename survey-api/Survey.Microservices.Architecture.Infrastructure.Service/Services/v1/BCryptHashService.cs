using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;

namespace Survey.Microservices.Architecture.Infrastructure.Service.Services.v1
{
    public class BCryptHashService : IHashService
    {
        public bool Compare(string hash, string value) =>
            BCrypt.Net.BCrypt.Verify(value, hash);

        public string Hash(string value) =>
            BCrypt.Net.BCrypt.HashPassword(value);
    }
}

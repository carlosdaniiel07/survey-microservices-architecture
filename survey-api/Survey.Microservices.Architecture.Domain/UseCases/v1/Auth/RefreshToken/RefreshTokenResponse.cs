namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

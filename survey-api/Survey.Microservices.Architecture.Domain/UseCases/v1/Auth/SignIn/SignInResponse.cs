namespace Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignIn
{
    public class SignInResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

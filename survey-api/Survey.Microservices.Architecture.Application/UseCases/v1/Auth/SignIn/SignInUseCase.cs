using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignIn;

namespace  Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignIn
{
    public class SignInUseCase : BaseUseCase<SignInUseCase>, ISignInUseCase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public SignInUseCase(
            ILogger<SignInUseCase> logger,
            INotificationContextService notificationContextService,
            IUnityOfWork unityOfWork,
            IHashService hashService,
            ITokenService tokenService
        ) : base(logger, notificationContextService)
        {
            _unityOfWork = unityOfWork;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<SignInResponse> ExecuteAsync(SignInRequest request)
        {
            try
            {
                var user = await _unityOfWork.UserRepository.GetByEmailAsync(request.Email);

                if (user == null)
                {
                    AddNotification("INVALID_CREDENTIALS");
                    return default;
                }

                var isValidPassword = _hashService.Compare(user.Password, request.Password);

                if (!isValidPassword)
                {
                    AddNotification("INVALID_CREDENTIALS");
                    return default;
                }

                if (!user.Active)
                {
                    AddNotification("USER_NOT_ACTIVE");
                    return default;
                }

                var accessToken = _tokenService.Generate(user);
                var refreshToken = Guid.NewGuid().ToString();

                return new SignInResponse(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while authenticating user @email", request.Email);
                throw;
            }
        }
    }
}

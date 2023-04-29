using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.Models.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignIn;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignIn
{
    public class SignInUseCase : BaseUseCase<SignInUseCase>, ISignInUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;
        private readonly ICacheService _cacheService;

        public SignInUseCase(
            ILogger<SignInUseCase> logger,
            INotificationContextService notificationContextService,
            IUserRepository userRepository,
            ITokenService tokenService,
            IHashService hashService,
            ICacheService cacheService
        ) : base(logger, notificationContextService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hashService = hashService;
            _cacheService = cacheService;
        }

        public async Task<SignInResponse> ExecuteAsync(SignInRequest request)
        {
            try
            {
                _logger.LogInformation("Authenticating user {email}", request.Email);

                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user is null)
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

                if (!user.IsActive)
                {
                    AddNotification("BLOCKED_USER");
                    return default;
                }

                var (accessToken, expirationInMinutes) = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _cacheService.AddAsync($"refresh-token:{refreshToken}", user, TimeSpan.FromMinutes(expirationInMinutes));

                _logger.LogInformation("Authenticated user {email}", request.Email);

                return new SignInResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while authenticating user {email}", request.Email);
                throw;
            }
        }
    }
}

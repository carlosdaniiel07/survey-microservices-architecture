using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.RefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Auth.RefreshToken
{
    public class RefreshTokenUseCase : BaseUseCase<RefreshTokenUseCase>, IRefreshTokenUseCase
    {
        private readonly ICacheService _cacheService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenUseCase(
            ILogger<RefreshTokenUseCase> logger,
            INotificationContextService notificationContextService,
            ICacheService cacheService,
            IUserRepository userRepository,
            ITokenService tokenService
        ) : base(logger, notificationContextService)
        {
            _cacheService = cacheService;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResponse> ExecuteAsync(RefreshTokenRequest request)
        {
            try
            {
                _logger.LogInformation("Refreshing token {refreshToken}", request.RefreshToken);

                var cacheKey = $"refresh-token:{request.RefreshToken}";
                var userFromCache = await _cacheService.RetrieveAsync<User>(cacheKey);

                if (userFromCache == null)
                {
                    AddNotification("INVALID_REFRESH_TOKEN");
                    return default;
                }

                var user = await _userRepository.GetByIdAsync(userFromCache.Id);
                var isUserActive = user?.IsActive ?? false;

                if (!isUserActive)
                {
                    AddNotification("BLOCKED_USER");
                    return default;
                }

                var (accessToken, expirationInMinutes) = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _cacheService.DeleteAsync(cacheKey);
                await _cacheService.AddAsync($"refresh-token:{refreshToken}", user, TimeSpan.FromMinutes(expirationInMinutes));

                _logger.LogInformation("Refreshed token {refreshToken}", request.RefreshToken);

                return new RefreshTokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while refreshing token {refreshToken}", request.RefreshToken);
                throw;
            }
        }
    }
}

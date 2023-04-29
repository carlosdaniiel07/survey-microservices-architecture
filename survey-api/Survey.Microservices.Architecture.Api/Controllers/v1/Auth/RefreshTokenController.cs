using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.RefreshToken;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/refresh-token")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;

        public RefreshTokenController(IRefreshTokenUseCase refreshTokenUseCase)
        {
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RefreshTokenResponse), 200)]
        public async Task<IActionResult> Post([FromBody] RefreshTokenRequest refreshTokenRequest) =>
            Ok(await _refreshTokenUseCase.ExecuteAsync(refreshTokenRequest));
    }
}

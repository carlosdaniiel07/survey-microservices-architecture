using Microsoft.AspNetCore.Mvc;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;

namespace Survey.Microservices.Architecture.Api.Controllers.v1.Auth
{
    [Route("api/v1/auth/sign-up")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpUseCase _signUpUseCase;

        public SignUpController(ISignUpUseCase signInUseCase)
        {
            _signUpUseCase = signInUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SignUpResponse), 200)]
        public async Task<IActionResult> Post([FromBody] SignUpRequest signUpRequest) =>
            Ok(await _signUpUseCase.ExecuteAsync(signUpRequest));
    }
}

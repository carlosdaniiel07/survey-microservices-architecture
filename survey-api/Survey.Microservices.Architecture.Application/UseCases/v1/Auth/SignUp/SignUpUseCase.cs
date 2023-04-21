using AutoMapper;
using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp
{
    public class SignUpUseCase : BaseUseCase<SignUpUseCase>, ISignUpUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public SignUpUseCase(
            ILogger<SignUpUseCase> logger,
            INotificationContextService notificationContextService,
            IMapper mapper,
            IUserRepository userRepository,
            IHashService hashService
        ) : base(logger, notificationContextService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<SignUpResponse> ExecuteAsync(SignUpRequest request)
        {
            try
            {
                _logger.LogInformation("Creating a new user {email}", request.Email);

                var alreadyExists = await _userRepository.AnyByEmailAsync(request.Email);

                if (alreadyExists)
                {
                    AddNotification("EMAIL_ALREADY_IN_USE");
                    return default;
                }

                var user = _mapper.Map<User>(request);
                user.Password = _hashService.Hash(request.Password);

                await _userRepository.AddAsync(user);

                _logger.LogInformation("Created a new user {email}", request.Email);

                return new SignUpResponse
                {
                    Id = user.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new user {email}", request.Email);
                throw;
            }
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;

namespace  Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp
{
    public class SignUpUseCase : BaseUseCase<SignUpUseCase>, ISignUpUseCase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public SignUpUseCase(
            ILogger<SignUpUseCase> logger,
            INotificationContextService notificationContextService,
            IUnityOfWork unityOfWork,
            IMapper mapper,
            IHashService hashService
        ) : base(logger, notificationContextService)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<SignUpResponse> ExecuteAsync(SignUpRequest request)
        {
            try
            {
                var alreadyExists = (await _unityOfWork.UserRepository.GetByEmailAsync(request.Email)) != null;

                if (alreadyExists)
                {
                    AddNotification("USER_ALREADY_EXISTS");
                    return default;
                }

                var user = _mapper.Map<User>(request);
                
                user.Password = _hashService.Hash(request.Password);

                await _unityOfWork.UserRepository.AddAsync(user);
                await _unityOfWork.CommitAsync();

                return _mapper.Map<SignUpResponse>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user @email", request.Email);
                throw;
            }
        }
    }
}

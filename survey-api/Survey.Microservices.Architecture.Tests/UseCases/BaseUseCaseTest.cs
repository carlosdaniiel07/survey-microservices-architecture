using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;

namespace  Survey.Microservices.Architecture.Tests.UseCases
{
    public abstract class BaseUseCaseTest<TUseCase>
    {
        protected readonly Mock<ILogger<TUseCase>> _loggerMock;
        protected readonly Mock<INotificationContextService> _notificationContextServiceMock;
        protected readonly IMapper _mapper;
        protected readonly Fixture _fixture;

        protected BaseUseCaseTest()
        {
            _loggerMock = new();
            _notificationContextServiceMock = new();
            _mapper = CreateMapper();
            _fixture = new Fixture();
        }

        private IMapper CreateMapper()
        {
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddMaps(typeof(SignUpProfile).Assembly);

            return new MapperConfiguration(mapperConfigurationExpression).CreateMapper();
        }

        protected abstract TUseCase MakeSut();
        protected abstract void SetupDefaultMocks();
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Microservices.Architecture.Application.Services.v1;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.RefreshToken;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignIn;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.AddSurveyAnswer;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.CreateSurvey;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.GetAllSurveys;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.GetSurveyResults;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.Models.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.RefreshToken;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignIn;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetAllSurveys;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetSurveyResults;
using Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Repositories.v1;
using Survey.Microservices.Architecture.Infrastructure.Service.Services.v1;

namespace Survey.Microservices.Architecture.Infrastructure.IoC
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDataServices(services);
            AddInfrastructureServices(services);
            AddApplicationServices(services);
            AddApplicationUseCases(services);
            AddAutoMapper(services);
            AddConfigurationModels(services, configuration);
        }

        private static void AddDataServices(IServiceCollection services)
        {
            #region MongoDb
            services.AddSingleton<ISurveyRepository, SurveyRepository>();
            services.AddSingleton<IAnswerRepository, AnswerRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            #endregion
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddTransient<IHashService, BCryptHashService>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContextService, NotificationContextService>();
            services.AddTransient<ISurveyService, SurveyService>();
        }

        private static void AddApplicationUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateSurveyUseCase, CreateSurveyUseCase>();
            services.AddScoped<IAddSurveyAnswerUseCase, AddSurveyAnswerUseCase>();
            services.AddScoped<IGetSurveyResultsUseCase, GetSurveyResultsUseCase>();
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
            services.AddScoped<IGetAllSurveysUseCase, GetAllSurveysUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateSurveyProfile).Assembly);
        }

        private static void AddConfigurationModels(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
        }
    }
}

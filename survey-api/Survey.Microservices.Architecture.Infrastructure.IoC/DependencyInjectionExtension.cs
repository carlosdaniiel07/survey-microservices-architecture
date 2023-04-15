using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Microservices.Architecture.Application.Services.v1;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.AddSurveyAnswer;
using Survey.Microservices.Architecture.Application.UseCases.v1.Survey.CreateSurvey;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories.v1;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.AddSurveyAnswer;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey;
using Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Repositories.v1;
using Survey.Microservices.Architecture.Infrastructure.Service.Services.v1;

namespace Survey.Microservices.Architecture.Infrastructure.IoC
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureBaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDataServices(services, configuration);
            AddInfrastructureServices(services);
            AddApplicationServices(services);
            AddApplicationUseCases(services);
            AddAutoMapper(services);
        }

        private static void AddDataServices(IServiceCollection services, IConfiguration configuration)
        {
            #region MongoDb
            services.AddSingleton<ISurveyRepository, SurveyRepository>();
            services.AddSingleton<IAnswerRepository, AnswerRepository>();
            #endregion
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContextService, NotificationContextService>();
        }

        private static void AddApplicationUseCases(IServiceCollection services)
        {
            services.AddScoped<ICreateSurveyUseCase, CreateSurveyUseCase>();
            services.AddScoped<IAddSurveyAnswerUseCase, AddSurveyAnswerUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateSurveyProfile).Assembly);
        }
    }
}

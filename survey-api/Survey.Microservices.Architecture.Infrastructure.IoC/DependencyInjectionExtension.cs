using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.Microservices.Architecture.Application.Services.v1;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignIn;
using Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp;
using Survey.Microservices.Architecture.Domain.Interfaces.Repositories;
using Survey.Microservices.Architecture.Domain.Interfaces.Services.v1;
using Survey.Microservices.Architecture.Domain.Models.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignIn;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;
using Survey.Microservices.Architecture.Infrastructure.Data;
using Survey.Microservices.Architecture.Infrastructure.Service.Services.v1;

namespace  Survey.Microservices.Architecture.Infrastructure.IoC
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
            AddConfigurationModels(services, configuration);
        }

        private static void AddDataServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite")));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<IHashService, BCryptHashService>();
            services.AddTransient<ITokenService, JwtTokenService>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<INotificationContextService, NotificationContextService>();
        }

        private static void AddApplicationUseCases(IServiceCollection services)
        {
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SignUpProfile).Assembly);
        }

        private static void AddConfigurationModels(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
        }
    }
}

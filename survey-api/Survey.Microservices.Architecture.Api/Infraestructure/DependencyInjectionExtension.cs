using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Survey.Microservices.Architecture.Api.Filters;
using Survey.Microservices.Architecture.Api.GraphQL.v1.Mutations;
using Survey.Microservices.Architecture.Api.GraphQL.v1.Queries;
using Survey.Microservices.Architecture.Domain.Models.v1;
using Survey.Microservices.Architecture.Infrastructure.IoC;
using System.Text;

namespace Survey.Microservices.Architecture.Api.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureBaseServices(builder.Configuration);

            AddGraphQL(builder.Services);
            AddControllers(builder.Services);
            AddSwagger(builder.Services);
            AddAuthenticationJwt(builder.Services, builder.Configuration);
        }

        private static void AddGraphQL(IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<SurveyQuery>()
                .AddMutationType<SurveyMutation>();
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add<NotificationFilter>();
            });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                        return new string[] { api.GroupName };

                    return new string[] { (api.ActionDescriptor as ControllerActionDescriptor).ControllerName };
                });

                config.DocInclusionPredicate((name, api) => true);
            });
        }

        private static void AddAuthenticationJwt(IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                };
            });
        }
    }
}

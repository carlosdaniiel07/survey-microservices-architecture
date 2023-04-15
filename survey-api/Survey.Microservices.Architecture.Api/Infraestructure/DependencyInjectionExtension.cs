﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Survey.Microservices.Architecture.Api.Filters;
using Survey.Microservices.Architecture.Infrastructure.IoC;

namespace Survey.Microservices.Architecture.Api.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureBaseServices(builder.Configuration);

            AddControllers(builder.Services);
            AddSwagger(builder.Services);
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
    }
}

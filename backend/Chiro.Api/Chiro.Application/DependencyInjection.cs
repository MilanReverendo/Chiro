using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chiro.Application.Interfaces;
using Chiro.Application.Mappers;

namespace Chiro.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here
            // e.g., services.AddScoped<IMyService, MyService>();

            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            //services
            services.AddScoped<IGroupMapper, GroupMapper>();
            services.AddScoped<IEventMapper, EventMapper>();
            services.AddScoped<IUserMapper, UserMapper>();
            return services;
        }
    }
}

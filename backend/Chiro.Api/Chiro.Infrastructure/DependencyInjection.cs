using Chiro.Application.Interfaces;
using Chiro.Infrastructure.Data;
using Chiro.Infrastructure.Repositories;
using Chiro.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chiro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Register DbContext
        services.AddDbContext<ChiroDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ChiroDatabase"));
        });

        // 2. Authentication configuration
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!)),
                    ValidateIssuerSigningKey = true,
                };
            });

        // 3. Register Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IEventService, EventService>();

        // register repository
        services.AddScoped<IBlobRepository, BlobRepository>();

        return services;
    }
}
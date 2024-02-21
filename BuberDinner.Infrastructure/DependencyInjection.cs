using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Persistence.Interceptors;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service,
        ConfigurationManager configuration)
    {
        service
            .AddAuth(configuration)
            .AddPersistence();

        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();


        return service;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection service)
    {
        service.AddDbContext<BuberDinnerDbContext>(options => options.UseSqlServer("Server=localhost;Database=BuperDinner;User Id=sa;Password=MyPass@word;Encrypt=false;"));

        service.AddScoped<PublishDomainEventsInterceptor>();

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IMenuRepository, MenuRepository>();

        return service;
    }

    private static IServiceCollection AddAuth(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        service.AddSingleton(Options.Create(jwtSettings));

        service.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return service;
    }
}
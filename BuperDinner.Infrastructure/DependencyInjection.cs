using BuperDinner.Application.Common.Interfaces.Authentication;
using BuperDinner.Application.Common.Interfaces.Persistence;
using BuperDinner.Application.Common.Interfaces.Services;
using BuperDinner.Infrastructure.Authentication;
using BuperDinner.Infrastructure.Persistence;
using BuperDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuperDinner.Infrastructure;

public static class DependencyInjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, ConfigurationManager configuration){
        
        service.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        service.AddSingleton<IJwtTokenGenerator,JwtTokenGenerator>();
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        service.AddScoped<IUserRepository, UserRepository>();
        return service;

   }
}
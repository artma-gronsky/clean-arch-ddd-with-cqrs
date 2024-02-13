using BuperDinner.Application.Services.Authentication;
using BuperDinner.Application.Services.Authentication.Commands;
using BuperDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuperDinner.Application;

public static class DependencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection service){
        
        return service
        .AddScoped<IAuthenticationCommandService, AuthenticationCommandService>()
        .AddScoped<IAuthenticationQueryService,AuthenticationQueryService>();
    }
}
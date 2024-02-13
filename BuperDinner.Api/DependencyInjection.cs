using BuperDinner.Api.Common.Errors;
using BuperDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuperDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // builder.Services.AddControllers(options =>{
        //     options.Filters.Add<ErrorHandlingFilterAttribute>();
        // });

        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, BuperDinnerProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}
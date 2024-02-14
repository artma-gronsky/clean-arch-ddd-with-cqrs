using System.Reflection;
using BuperDinner.Application.Authentication.Commands.Register;
using BuperDinner.Application.Authentication.Common;
using BuperDinner.Application.Common.Behaviors;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuperDinner.Application;

public static class DependencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        // register mediatR with command and queries
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        // reguster behavior pipeline
        services.AddScoped<IPipelineBehavior<
            RegisterCommand, ErrorOr<AuthenticationResult>>, 
            ValidateRegisterCommandBehavior>();

         // register validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // AddValidatorsFromAssembly or each validators one by one
       // services.AddScoped<AbstractValidator<RegisterCommand>, RegisterCommandValidator>();    

        return services;
    }
}
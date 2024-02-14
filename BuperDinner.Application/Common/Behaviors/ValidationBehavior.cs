using BuperDinner.Application.Authentication.Commands.Register;
using BuperDinner.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuperDinner.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command,
                                                       RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
                                                       CancellationToken cancellationToken)
    {
    
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid)
        {
            var result = await next();

            return result;
        }
        
        var errors = validationResult.Errors
            .ConvertAll(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));

        return errors;
    }
}
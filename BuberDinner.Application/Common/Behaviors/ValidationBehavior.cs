using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(
        TRequest command,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            var result = await next();

            return result;
        }

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid)
        {
            var result = await next();

            return result;
        }

        var errors = validationResult.Errors
            .ConvertAll(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage));

        // return (TResponse?)typeof(TResponse)
        //     .GetMethod(
        //         name: nameof(ErrorOr<object>.From),
        //         bindingAttr: BindingFlags.Static | BindingFlags.Public,
        //         types: new[] { typeof(List<Error>) })?
        //     .Invoke(null, new[] { errors })!;
        return (dynamic)errors;
    }
}
using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuperDinner.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
        where TRequest: IRequest<TResponse>
        where TResponse: IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
            TRequest command,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
    {
        if(_validator is null){
            var result = await next();

            return result;
        }

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

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
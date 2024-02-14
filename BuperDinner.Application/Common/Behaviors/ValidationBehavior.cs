using BuperDinner.Application.Authentication.Commands.Register;
using BuperDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuperDinner.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, IErrorOr<AuthenticationResult>>
{
    public Task<IErrorOr<AuthenticationResult>> Handle(RegisterCommand request, RequestHandlerDelegate<IErrorOr<AuthenticationResult>> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
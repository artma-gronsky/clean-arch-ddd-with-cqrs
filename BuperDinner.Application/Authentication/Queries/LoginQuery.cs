using BuperDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuperDinner.Application.Authentication.Queries;

public record LoginQuery(
    string Email,
    string Password
    ): IRequest<ErrorOr<AuthenticationResult>>;
using BuperDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuperDinner.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName,
                              string LastName,
                              string Email,
                              string Password)
                                        // returns
                              :IRequest<ErrorOr<AuthenticationResult>>;
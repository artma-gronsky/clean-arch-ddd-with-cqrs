using BuperDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuperDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}
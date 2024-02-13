using BuperDinner.Application.Services.Authentication.Common;
using BuperDinner.Domain.Entities;
using ErrorOr;
using MediatR;
using BuperDinner.Domain.Common.Errors;
using BuperDinner.Application.Common.Interfaces.Authentication;
using BuperDinner.Application.Common.Interfaces.Persistence;

namespace BuperDinner.Application.Authentication.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
    {
        // Check if user already exist
        if (_userRepository.GetUserByEmail(command.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != command.Password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        // Create JWT token 
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}

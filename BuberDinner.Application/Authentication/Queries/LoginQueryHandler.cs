using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Users;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries;

public class LoginQueryHandler : 
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
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
        await Task.CompletedTask;
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

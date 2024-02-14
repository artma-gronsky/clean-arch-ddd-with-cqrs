using BuperDinner.Application.Common.Interfaces.Authentication;
using BuperDinner.Application.Common.Interfaces.Persistence;
using BuperDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;
using BuperDinner.Domain.Entities;
using BuperDinner.Domain.Common.Errors;

namespace BuperDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
         // Check if user already exist
        if(_userRepository.GetUserByEmail(command.Email) is not null){
            return Errors.User.DuplicateEmailError;
        }

        //Create user (generate unique Id)
        var user = new User(){
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.AddUser(user);

        // Create JWT token 
        var token = _jwtTokenGenerator.GenerateToken(user);

         return new AuthenticationResult(user, token);
    }
}


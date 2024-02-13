using BuperDinner.Application.Common.Interfaces.Authentication;
using BuperDinner.Application.Common.Interfaces.Persistence;
using BuperDinner.Application.Services.Authentication.Common;
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

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
         // Check if user already exist
        if(_userRepository.GetUserByEmail(request.Email) is not null){
            return Errors.User.DuplicateEmailError;
        }

        //Create user (generate unique Id)
        var user = new User(){
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        _userRepository.AddUser(user);

        // Create JWT token 
        var token = _jwtTokenGenerator.GenerateToken(user);

         return new AuthenticationResult(user, token);
    }
}


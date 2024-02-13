using BuperDinner.Application.Common.Interfaces.Authentication;
using BuperDinner.Application.Common.Interfaces.Persistence;
using BuperDinner.Application.Services.Authentication.Common;
using BuperDinner.Domain.Common.Errors;
using BuperDinner.Domain.Entities;
using ErrorOr;

namespace BuperDinner.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exist
        if(_userRepository.GetUserByEmail(email) is not null){
            return Errors.User.DuplicateEmailError;
        }

        //Create user (generate unique Id)
        var user = new User(){
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.AddUser(user);

        // Create JWT token 
        var token = _jwtTokenGenerator.GenerateToken(user);

         return new AuthenticationResult(user, token);
    }
}
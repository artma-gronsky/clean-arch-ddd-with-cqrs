using BuperDinner.Application.Services.Authentication;
using BuperDinner.Application.Services.Authentication.Commands;
using BuperDinner.Application.Services.Authentication.Common;
using BuperDinner.Application.Services.Authentication.Queries;
using BuperDinner.Contracts.Authentications;
using BuperDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuperDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{

    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(IAuthenticationCommandService authenticationService, IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationService;
        _authenticationQueryService = authenticationQueryService;
    }

    // [HttpPost("register")]
    // public IActionResult Register(RegisterRequest reqiest)
    // {
    //     OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(reqiest.FirstName, reqiest.LastName, reqiest.Email, reqiest.Password);

    //     return registerResult.Match(
    //         authResult =>
    //         {
    //             AuthenticationResponse response = MapAuthResult(authResult);
    //             return Ok(response);
    //         },
    //         error => Problem(statusCode: (int)error.StausCode, title: error.Message) 
    //     );
    // }

    // [HttpPost("register")]
    // public IActionResult Register(RegisterRequest reqiest)
    // {
    //     Result<AuthenticationResult> registerResult = _authenticationService.Register(reqiest.FirstName, reqiest.LastName, reqiest.Email, reqiest.Password);

    //     if(registerResult.IsSuccess){
    //         return Ok(MapAuthResult(registerResult.Value));
    //     }

    //     var firstError = registerResult.Errors[0];

    //     if(firstError is DuplicateEmailError){
    //         return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exsists.");
    //     }

    //     return Problem();
    // }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest reqiest)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationCommandService.Register(reqiest.FirstName, reqiest.LastName, reqiest.Email, reqiest.Password);

        // return registerResult.Match(
        //         authResult =>
        //                     {
        //                         AuthenticationResponse response = MapAuthResult(authResult);
        //                         return Ok(response);
        //                     },
        //         _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "User already exsists."));

        return registerResult.MatchFirst(
            authResult => Ok(MapAuthResult(authResult)),
            Problem);
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                               authResult.User.Id,
                               authResult.User.FirstName,
                               authResult.User.LastName,
                               authResult.User.Email,
                               authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest reqiest)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationQueryService.Login(reqiest.Email, reqiest.Password);

        if(registerResult.IsError && registerResult.FirstError == Errors.Authentication.InvalidCredentials){
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: registerResult.FirstError.Description);
        }

       return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
}
using BuperDinner.Application.Authentication.Commands.Register;
using BuperDinner.Application.Authentication.Queries;
using BuperDinner.Application.Authentication.Common;
using BuperDinner.Contracts.Authentications;
using BuperDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace BuperDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
    public async Task<IActionResult> Register(RegisterRequest reqiest)
    {
        var registerCommand = _mapper.Map<RegisterCommand>(reqiest);

        // return registerResult.Match(
        //         authResult =>
        //                     {
        //                         AuthenticationResponse response = MapAuthResult(authResult);
        //                         return Ok(response);
        //                     },
        //         _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "User already exsists."));

        var authResult = await _mediator.Send(registerCommand);
        return authResult.MatchFirst(
            result => Ok(_mapper.Map<AuthenticationResponse>(result)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest reqiest)
    {
        var loginQuery = _mapper.Map<LoginQuery>(reqiest);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(loginQuery);

        if(registerResult.IsError && registerResult.FirstError == Errors.Authentication.InvalidCredentials){
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: registerResult.FirstError.Description);
        }

       return registerResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
}
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuperDinner.Api.Controllers;

public class ErrorsController: ControllerBase{
    [Route("/error")]
    public IActionResult Error(){
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch{
            _=> (StatusCodes.Status500InternalServerError, "An unhandked exception occurred.")
        };

        return Problem(title: message, statusCode: statusCode);
        // return Problem(title: exception?.Message, statusCode: 400);
    }
}
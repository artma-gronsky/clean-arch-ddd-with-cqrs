using BuberDinner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic;

namespace BuberDinner.Api.Controllers;


[ApiController]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if(errors == null || errors.Count is 0){
            return Problem();   
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDic = new ModelStateDictionary();

        foreach (Error error in errors)
        {
            modelStateDic.AddModelError(
                error.Code,
                error.Description
            );
        }

        return ValidationProblem(modelStateDic);
    }
}
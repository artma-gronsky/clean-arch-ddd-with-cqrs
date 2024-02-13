using BuperDinner.Api.Common.Errors;
using BuperDinner.Application;
using BuperDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers(options =>{
    //     options.Filters.Add<ErrorHandlingFilterAttribute>();
    // });

     builder.Services.AddControllers();

     builder.Services.AddSingleton<ProblemDetailsFactory, BuperDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiuddleware>();
    app.UseExceptionHandler("/error");
    // app.Map("/error", (HttpContext httpContext) => {
    //     Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //     return Results.Problem();
    // });
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
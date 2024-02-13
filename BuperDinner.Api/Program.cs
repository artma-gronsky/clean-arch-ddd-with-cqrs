using BuperDinner.Api;
using BuperDinner.Application;
using BuperDinner.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
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
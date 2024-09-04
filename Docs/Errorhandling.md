Implementing Error handling

1. Middleware Approach:

Create an error handling middleware class

```
  public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new { error = ex.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
```

Add this class/type in the services container, requests would arrive and reach this middleware via the middleware pipeline

```
app.UseMiddleware<ErrorHandlingMiddleware>();
```

This in turn would call the next middleware in the pipeline and if any exception is met falling into the catch which then call the private HandleExceptionAync method:

```
  public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
```

Which is then handled by:

```
 private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new { error = ex.Message });
            return context.Response.WriteAsync(result);
        }
```

2. Filter Attribute Approach:
Create a class inherting from ExceptionFilterAttribute:

```
public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemdetails = new ProblemDetails
        {
            Type= "https://tools.ietf.org/html/rfc7321#section-6.6.1",
            Title = "An error occurred while processing your request",
            Status = StatusCodes.Status500InternalServerError,
            Detail = context.Exception.Message
        };
        context.Result = new JsonResult(problemdetails);
        context.ExceptionHandled = true;
    }
}
```

This filter can then be added as an attribute on the controllers in concern

```
 [ApiController]
    [Route("auth")]
    [ErrorHandlingFilter]
    public class AuthenticationController : ControllerBase
    {

    }

```

However alternative approach would be to add the filter attribute to all controllers by adding to services container

```
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
}
```
3. Global Error handling Approach
which is when an error occurs to re-route the execption to a new/global endpoint 
```
var app = builder.Build();
{    
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
```
In order to implement this approach and error controller is needed 
```
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(statusCode: 500, detail: exception?.Message);
    }

}
```
This approach and the Problem() method can be extended to include custom properties and values by coping the DefaultProblemDetailsFactory class from Asp.net framework classes on github and implement your own ProblemDetailsFactory and add your own properties via the:
```
problemDetails.Extensions.Add("customProperty","customValue");
```
Then override the default by adding this to the services container:
```
builder.Serivces.AddSingleton<ProblemDetailsFactory, MyProblemDetailsFactory>();
```
This would then return the expected exception plus the custom properties in the returned Json:
```
{
  "type": "https://tools.ietf.org/html/rfc7321#section-6.6.1",
  "title": "An error occurred while processing your request",
  "status": 500,
  "detail": "user already exist"
  "customProperty": "customValue"
}
```
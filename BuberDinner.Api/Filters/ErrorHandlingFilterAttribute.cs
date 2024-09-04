using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace BuberDinner.Api.Filters;

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
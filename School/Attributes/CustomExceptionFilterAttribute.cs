// Custom exception filter attribute
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Console.WriteLine($"An error occurred: {context.Exception.Message}");
        context.HttpContext.Response.ContentType = "text/plain";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.HttpContext.Response.WriteAsync(context.Exception.Message);
        context.ExceptionHandled = true;
    }
}

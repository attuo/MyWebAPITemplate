using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyWebAPITemplate.Source.Core.Exceptions;
using Newtonsoft.Json;

namespace MyWebAPITemplate.Source.Web.Middlewares;

/// <summary>
/// Global error handling.
/// Gets called each time when system throws and exception.
/// Acts as global try-catch.
/// </summary>
public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalErrorHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">Request delegate for the middleware.</param>
    /// <param name="logger">Logger for logging the errors.</param>
    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Try-catch for handling the exceptions.
    /// </summary>
    /// <param name="context">See <see cref="HttpContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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

    /// <summary>
    /// Determines the <see cref="HttpStatusCode"/> based on the exception.
    /// </summary>
    /// <param name="ex">Exception that has been thrown.</param>
    /// <param name="logger">Logger instance.</param>
    /// <returns>Suitable <see cref="HttpStatusCode"/>.</returns>
    private static HttpStatusCode DetermineStatusCode(Exception ex, ILogger logger)
    {
        switch (ex)
        {
            case ArgumentNullException exception:
                LogError(exception.GetType(), exception);
                return HttpStatusCode.InternalServerError;
            case EntityNotFoundException exception:
                LogError(exception.GetType(), exception);
                return HttpStatusCode.NotFound;
            case Exception exception:
                LogError(exception.GetType(), exception);
                return HttpStatusCode.InternalServerError;
            default:
                return HttpStatusCode.InternalServerError;
        }

        void LogError(Type type, Exception ex)
            => logger.LogError("Unexpected error: {ExceptionType}. {ExceptionContent}", type, ex.ToString());
    }

    /// <summary>
    /// Determines what kind of error message is returned to endpoint caller.
    /// </summary>
    /// <param name="context">See <see cref="HttpContext"/>.</param>
    /// <param name="ex">Any exception that was thrown.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // TODO: Do not return raw exception message on other environments than development
        var errorCode = DetermineStatusCode(ex, _logger);

        // TODO: Choose what kind of error messages to be sent when in production.
        var result = JsonConvert.SerializeObject(new { error = ex.ToString() });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorCode;
        await context.Response.WriteAsync(result);
    }
}
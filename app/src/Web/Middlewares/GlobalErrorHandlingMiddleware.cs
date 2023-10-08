using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyWebAPITemplate.Source.Core.Exceptions;
using Serilog;

namespace MyWebAPITemplate.Source.Web.Middlewares;

/// <summary>
/// Global error handling.
/// Gets called each time when system throws and exception.
/// Acts as global try-catch.
/// </summary>
public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logger = Log.ForContext<GlobalErrorHandlingMiddleware>();

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalErrorHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">Request delegate for the middleware.</param>
    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Try-catch for handling the exceptions.
    /// </summary>
    /// <param name="context">See <see cref="HttpContext"/>.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when method parameter is null.</exception>
    public async Task InvokeAsync(HttpContext context)
    {
        _ = context ?? throw new ArgumentNullException(nameof(context));
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
    private static HttpStatusCode DetermineStatusCode(Exception ex, Serilog.ILogger logger)
    {
        switch (ex)
        {
            case ArgumentNullException exception:
                LogError(exception);
                return HttpStatusCode.InternalServerError;

            case EntityNotFoundException exception:
                LogError(exception);
                return HttpStatusCode.NotFound;

            case Exception exception:
                LogError(exception);
                return HttpStatusCode.InternalServerError;

            default:
                return HttpStatusCode.InternalServerError;
        }

        void LogError(Exception ex)
            => logger.Error("Error: {ExceptionType}. {ExceptionMessage}", ex.GetType(), ex.Message);
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
        var result = JsonSerializer.Serialize(new { error = ex.ToString() });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorCode;
        await context.Response.WriteAsync(result);
    }
}
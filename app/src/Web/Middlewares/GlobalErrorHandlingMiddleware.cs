using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyWebAPITemplate.Source.Web.Middlewares
{
    /// <summary>
    /// Global error handling
    /// Gets called each time when system throws and exception
    /// Acts as global try-catch
    /// </summary>
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Try-catch for handling the exceptions
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
        /// Determines what kind of error message is returned to endpoint caller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // TODO: Do not return raw exception message on other environments than development

            var errorCode = HttpStatusCode.InternalServerError;


            if (ex is ArgumentNullException)
            {
                LogError(typeof(ArgumentNullException), ex);
                errorCode = HttpStatusCode.InternalServerError;
            }
            if (ex is Exception)
            {
                LogError(typeof(Exception), ex);
                errorCode = HttpStatusCode.InternalServerError;
            }


            string errorMessage = ""; // TODO: Choose what kind of error messages to be sent when in production

            var result = JsonConvert.SerializeObject(new { error = ex.ToString() });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorCode;
            await context.Response.WriteAsync(result);

            void LogError(Type type, Exception ex) 
            {
                _logger.LogError("Unexpected error: {ExceptionType}. {ExceptionContent}", type, ex.ToString());
            }
        }
    }
}

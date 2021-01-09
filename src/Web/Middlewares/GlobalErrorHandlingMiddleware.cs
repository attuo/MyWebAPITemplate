using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly RequestDelegate next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
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
                await next(context);
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
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (ex is NotFoundException) code = HttpStatusCode.NotFound;
            if (ex is Exception) code = HttpStatusCode.BadRequest;

            string errorMessage = ""; // TODO: Choose what kind of error messages to be sent when in production

            var result = JsonConvert.SerializeObject(new { error = errorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

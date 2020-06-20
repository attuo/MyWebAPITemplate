using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspNetCoreWebApiTemplate.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

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

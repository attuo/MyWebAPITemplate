//using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using MyWebAPITemplate.Source.Web.Middlewares;
using Serilog;

namespace MyWebAPITemplate.Extensions
{
    /// <summary>
    /// Contains all the application builder extension methods for configuring the system
    /// This is the class for all kind of registrations for IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configs for local development
        /// Contains development tool and other development related settings
        /// Should be run first
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureDevelopmentSettings(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseMigrationsEndPoint()
                    .UseCors();
                //.UseShowAllServicesMiddleware()
                //.UseDatabaseErrorPage();
            }

            return app;
        }

        /// <summary>
        /// Swagger configurations
        /// Should be run before routing configurations
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API V1");
            });

            return app;
        }

        /// <summary>
        /// Logger configurations
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureLogger(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }

        /// <summary>
        /// Middleware usings
        /// All the middlewares should be registered here
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Global error handling
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

        /// <summary>
        /// Used for applying different CORS settings
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCors(this IApplicationBuilder app)
        {
            app.UseCors("AnyOrigin"); // TODO: Remember to change this when more specific CORS settings are configured

            return app;
        }

        /// <summary>
        /// Routing related configurations
        /// This should be run last in Startup.cs configure
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapAllHealthChecks();
                routeBuilder.MapControllers();
            });

            return app;
        }

        private static void MapAllHealthChecks(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapHealthChecks("/health");
            //routeBuilder.MapHealthChecksUI();
        }

    }
}

using Ardalis.ListStartupServices;
using MyWebAPITemplate.Source.Web.Extensions;
using MyWebAPITemplate.Source.Web.Middlewares;
using Serilog;

namespace MyWebAPITemplate.Source.Web.Extensions;

/// <summary>
/// Contains all the application builder extension methods for configuring the system.
/// This is the class for all kind of registrations for IApplicationBuilder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configs for local development.
    /// Contains development tool and other development related settings.
    /// Should be run first.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">See <see cref="RunningEnvironment"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder ConfigureDevelopmentSettings(this IApplicationBuilder app, RunningEnvironment env)
        => env.IsLocalDevelopment()
            ? app
                .UseDeveloperExceptionPage()
                .UseMigrationsEndPoint()
                .UseCors()
                .UseShowAllServicesMiddleware()
            : app;

    /// <summary>
    /// Swagger configurations
    /// Should be run before routing configurations.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API V1"));

    /// <summary>
    /// Logger configurations.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder ConfigureLogger(this IApplicationBuilder app)
        => app
            .UseSerilogRequestLogging();

    /// <summary>
    /// Middleware usings
    /// All the middlewares should be registered here.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        => app.
            UseMiddleware<GlobalErrorHandlingMiddleware>(); // Global error handling

    /// <summary>
    /// Used for applying different CORS settings.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseCors(this IApplicationBuilder app)
        => app
            .UseCors("AnyOrigin"); // TODO: Remember to change this when more specific CORS settings are configured

    /// <summary>
    /// Routing related configurations.
    /// This should be run last in Startup.cs configure.
    /// </summary>
    /// <param name="app">See <see cref="IApplicationBuilder"/>.</param>
    /// <returns>Same instance of <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
        => app
            .UseHttpsRedirection()
            .UseRouting()
            .UseEndpoints(routeBuilder =>
            {
                _ = routeBuilder.MapHealthChecks("/health");
                _ = routeBuilder.MapHealthChecksUI();
                _ = routeBuilder.MapControllers();
            });
}
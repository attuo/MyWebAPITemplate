using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace MyWebAPITemplate.Source.Web.Extensions;

/// <summary>
/// Extension methods for <see cref="IWebHostBuilder"/>.
/// </summary>
public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Configures the Serilog to the system.
    /// </summary>
    /// <param name="webHostBuilder">See <see cref="IWebHostBuilder"/>.</param>
    /// <param name="env">See <see cref="RunningEnvironment"/>.</param>
    /// <returns>Same instance of see <see cref="IWebHostBuilder"/>.</returns>
    public static IWebHostBuilder UseConfiguredSerilog(this IWebHostBuilder webHostBuilder, RunningEnvironment env)
        => webHostBuilder
            .UseSerilog((hostingContext, loggerConfiguration)
                => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                    .Enrich.WithProperty("Environment", env.Name));
}
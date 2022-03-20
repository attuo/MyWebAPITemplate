using MyWebAPITemplate.Source.Web.Interfaces;
using Serilog;

namespace MyWebAPITemplate.Source.Web.Extensions;

/// <summary>
/// Extension methods for <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures the Serilog to the system.
    /// </summary>
    /// <param name="hostBuilder">See <see cref="IHostBuilder"/>.</param>
    /// <param name="env">See <see cref="RunningEnvironment"/>.</param>
    /// <returns>Same instance of see <see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder UseConfiguredSerilog(this IHostBuilder hostBuilder, IRunningEnvironment env)
        => hostBuilder
            .UseSerilog((hostingContext, loggerConfiguration)
                => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                    .Enrich.WithProperty("Environment", env.Name));
}
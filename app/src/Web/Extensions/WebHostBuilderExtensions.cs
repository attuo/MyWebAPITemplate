using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace MyWebAPITemplate.Source.Web.Extensions;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder UseConfiguredSerilog(this IWebHostBuilder webHostBuilder, RunningEnvironment env)
        => webHostBuilder
            .UseSerilog((hostingContext, loggerConfiguration)
                => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                    .Enrich.WithProperty("Environment", env.Name));
}

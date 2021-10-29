using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebAPITemplate.Extensions;
using MyWebAPITemplate.Source.Web.Extensions;

namespace MyWebAPITemplate.Source.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets automatically called by the runtime. 
        /// Contains configuration calls for IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // All the configurations are made with extension methods
            // Configurations are located in Web/Extensions/ServiceCollectionExtensions
            services
                .ConfigureDatabase(Configuration)
                .ConfigureDevelopmentSettings()
                .ConfigureCors()
                .ConfigureSwagger()
                .ConfigureHealthChecks(Configuration)
                .ConfigureSettings(Configuration)
                .AddApplicationServices()
                .AddApplicationMappers()
                .AddApplicationRepositories()
                .AddModelValidators()
                .AddControllers()
                .AddFluentValidation(); // this must be called directly after AddControllers
        }


        /// <summary>
        /// This method gets automatically called by the runtime. 
        /// Contains configuration calls for ApplicationBuilder
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .ConfigureDevelopmentSettings(env)
                .ConfigureSwagger()
                .ConfigureLogger()
                .ConfigureRouting(); // This usually must be called the last
        }
    }
}

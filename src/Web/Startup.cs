using AspNetCoreWebApiTemplate.Extensions;
using AspNetCoreWebApiTemplate.Web.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreWebApiTemplate.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureDatabase(Configuration)
                .ConfigureDevelopmentSettings()
                .ConfigureSwagger()
                .AddApplicationServices()
                .AddApplicationConverters()
                .AddApplicationRepositories()
                .AddModelValidators()
                .AddControllers()
                .AddFluentValidation(); // this must be called directly after AddControllers
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .ConfigureDevelopmentSettings(env)
                .ConfigureSwagger()
                .ConfigureRouting();
        }
    }
}

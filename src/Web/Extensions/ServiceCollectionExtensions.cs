using System;
using System.IO;
using System.Reflection;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;
using AspNetCoreWebApiTemplate.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AspNetCoreWebApiTemplate.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Add Application Services and Options

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            AddApplicationInternalServices(services);
            AddApplicationExternalServices(services);
            return services;
        }

        /// <summary>
        /// Services from ApplicationCore/Services 
        /// </summary>
        /// <param name="services"></param>
        private static void AddApplicationInternalServices(IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        private static void AddApplicationExternalServices(IServiceCollection services)
        {

        }

        #endregion

        #region Configure Swagger

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            // Read more https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Template API",
                    Description = "This is a template for dotnet.",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        #endregion


    }
}

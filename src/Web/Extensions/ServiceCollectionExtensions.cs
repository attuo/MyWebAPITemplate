using System;
using System.IO;
using System.Reflection;
using AspNetCoreWebApiTemplate.ApplicationCore.Converter;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Converters;
using AspNetCoreWebApiTemplate.ApplicationCore.Services;
using AspNetCoreWebApiTemplate.Web.Converters;
using AspNetCoreWebApiTemplate.Web.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AspNetCoreWebApiTemplate.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Dependency Injection for Application Services and Options

        /// <summary>
        /// Dependency injections for Application Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            AddInternalServices(services);
            AddExternalServices(services);
            return services;
        }

        /// <summary>
        /// Services from ApplicationCore/Services 
        /// </summary>
        /// <param name="services"></param>
        private static void AddInternalServices(IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }

        /// <summary>
        /// Services from Infrastructure/Services
        /// </summary>
        /// <param name="services"></param>
        private static void AddExternalServices(IServiceCollection services)
        {

        }

        #region Dependency Injection for Converters 

        /// <summary>
        /// Dependency Injections for Application Converters
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationConverters(this IServiceCollection services)
        {
            // The idea for converter DI methods are derived from here: 
            // https://softwareengineering.stackexchange.com/questions/301580/best-practices-regarding-type-mapping-and-extension-methods
            AddModelDtoConverters(services);
            AddDtoEntityConverters(services);
            return services;
        }

        private static void AddModelDtoConverters(IServiceCollection services)
        {
            services.AddScoped<ITodoModelDtoConverter, TodoModelDtoConverter>();
        }

        private static void AddDtoEntityConverters(IServiceCollection services)
        {
            services.AddScoped<ITodoDtoEntityConverter, TodoDtoEntityConverter>();
        }

        #endregion

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
                    Description = "This is the documentation of this API",
                    Contact = new OpenApiContact
                    {
                        Name = "The Developer",
                        Email = "test@test.test"
                    },
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

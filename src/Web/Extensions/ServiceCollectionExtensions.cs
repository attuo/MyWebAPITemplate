using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ardalis.ListStartupServices;
using AspNetCoreWebApiTemplate.ApplicationCore.Converter;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Converters;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Database;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.InternalServices;
using AspNetCoreWebApiTemplate.ApplicationCore.Services;
using AspNetCoreWebApiTemplate.Infrastructure.Database;
using AspNetCoreWebApiTemplate.Infrastructure.Database.Repositories;
using AspNetCoreWebApiTemplate.Web.Converters;
using AspNetCoreWebApiTemplate.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        #region Dependency Injection for Repositories

        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }

        #endregion

        #region Configure Database

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //ConfigureInMemoryDatabase(services);
            ConfigureSQLServerDatabase(services, configuration);
            return services;
        }

        private static void ConfigureInMemoryDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(builder => builder.UseInMemoryDatabase("APITemplateDatabase"));
        }

        private static void ConfigureSQLServerDatabase(IServiceCollection services, IConfiguration configuration)
        {
            // Requires LocalDB which can be installed with SQL Server Express
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("SQLServerConnection")));
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

        #region Configure Development Settings

        #endregion

        public static IServiceCollection ConfigureDevelopmentSettings(this IServiceCollection services)
        {
            // TODO: Environment checking here
            ConfigureListStartupServicesMiddleware(services);
            return services;
        }

        private static void ConfigureListStartupServicesMiddleware(IServiceCollection services)
        {
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);
                config.Path = "/listservices";
            });
        }

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ardalis.ListStartupServices;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyWebAPITemplate.Source.Core.Interfaces.Database;
using MyWebAPITemplate.Source.Core.Interfaces.InternalServices;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;
using MyWebAPITemplate.Source.Core.Mappers;
using MyWebAPITemplate.Source.Core.Services;
using MyWebAPITemplate.Source.Infrastructure.Database;
using MyWebAPITemplate.Source.Infrastructure.Database.Repositories;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Validators;

namespace MyWebAPITemplate.Source.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Dependency Injection for Application's Services

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
        /// Services from Core/Services 
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

        #endregion

        #region Dependency Injection for Application's Mappers 

        /// <summary>
        /// Dependency Injections for application's model mappers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationMappers(this IServiceCollection services)
        {
            // The idea for mapper DI methods are derived from here: 
            // https://softwareengineering.stackexchange.com/questions/301580/best-practices-regarding-type-mapping-and-extension-methods
            AddModelDtoMappers(services);
            AddDtoEntityMappers(services);
            return services;
        }

        /// <summary>
        /// Mappers from Web/Mappers
        /// </summary>
        /// <param name="services"></param>
        private static void AddModelDtoMappers(IServiceCollection services)
        {
            services.AddScoped<ITodoModelDtoMapper, TodoModelDtoMapper>();
        }

        /// <summary>
        /// Mappers from Core/Mappers
        /// </summary>
        /// <param name="services"></param>
        private static void AddDtoEntityMappers(IServiceCollection services)
        {
            services.AddScoped<ITodoDtoEntityMapper, TodoDtoEntityMapper>();
        }

        #endregion

        #region Dependency Injection for Application's Repositories

        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }

        #endregion

        #region Dependency Injection for Application's Validators (FluentValidation)

        public static IServiceCollection AddModelValidators(this IServiceCollection services)
        {
            // Register the validators here
            services.AddTransient<IValidator<TodoRequestModel>, TodoRequestModelValidator>();
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

        #region Configure Cors

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            // TODO: Change the Allow Any for more strict
            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            return services;
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

        #region Configure Development Settings

        public static IServiceCollection ConfigureDevelopmentSettings(this IServiceCollection services)
        {
            // TODO: Environment checking here
            ConfigureListStartupServices(services);
            services.AddDatabaseDeveloperPageExceptionFilter(); // https://github.com/aspnet/Announcements/issues/432
            return services;
        }

        private static void ConfigureListStartupServices(IServiceCollection services)
        {
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);
                config.Path = "/listservices";
            });
        }

        #endregion

        #region Configure HealthChecks

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
            //services.AddHealthChecksUI(setup =>
            //{
            //    setup.AddHealthCheckEndpoint("Health", "https://localhost:5001/health");
            //})
            //    .AddSqlServerStorage(configuration.GetConnectionString("SQLServerConnection"));

            return services;
        }

        #endregion
    }
}

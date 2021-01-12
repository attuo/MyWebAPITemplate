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
using MyWebAPITemplate.Source.Web.Options;
using MyWebAPITemplate.Source.Web.Validators;

namespace MyWebAPITemplate.Source.Web.Extensions
{
    /// <summary>
    /// Contains all the service collection extension methods for configuring the system
    /// This is the class for all kind of registerations for IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Dependency Injection for Application's Services

        /// <summary>
        /// Dependency injections for all the Application Services
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
        /// Dependency injections for services from Core/Services
        /// When creating a new service in core project, remember to register it here
        /// </summary>
        /// <param name="services"></param>
        private static void AddInternalServices(IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }

        /// <summary>
        /// Dependency injections for services from Infrastructure/Services
        /// When creating a new service in infrastructure project, remember to register it here
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
        /// Dependency Injections for mappers from Web/Mappers
        /// When creating a new mapper in Web project, remember to register it here
        /// </summary>
        /// <param name="services"></param>
        private static void AddModelDtoMappers(IServiceCollection services)
        {
            services.AddScoped<ITodoModelDtoMapper, TodoModelDtoMapper>();
        }

        /// <summary>
        /// Dependency Injections for mappers from Core/Mappers
        /// When creating a new mapper in Core project, remember to register it here
        /// </summary>
        /// <param name="services"></param>
        private static void AddDtoEntityMappers(IServiceCollection services)
        {
            services.AddScoped<ITodoDtoEntityMapper, TodoDtoEntityMapper>();
        }

        #endregion

        #region Dependency Injection for Application's Repositories

        /// <summary>
        /// Dependency Injections for repositories from Infrastructure/Database/Repositories
        /// When creating a new repository, remember to register it here
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }

        #endregion

        #region Dependency Injection for Application's Validators (FluentValidation)

        /// <summary>
        /// Dependency Injections for validators from Web/Validators
        /// When creating a new validator, remember to register it here
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModelValidators(this IServiceCollection services)
        {
            // Register the validators here
            services.AddTransient<IValidator<TodoRequestModel>, TodoRequestModelValidator>();
            return services;
        }

        #endregion

        #region Configure Database

        /// <summary>
        /// Configurations for databases
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureSQLServerDatabase(services, configuration);
            return services;
        }

        /// <summary>
        /// Configurations for SQL Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void ConfigureSQLServerDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var databaseOptions = new DatabaseSettings();
            configuration.GetSection(DatabaseSettings.OptionsName).Bind(databaseOptions);

            // "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=TemplateDb;"

            // Requires LocalDB which can be installed with SQL Server Express
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(databaseOptions.ConnectionString));
        }

        #endregion

        #region Configure Cors

        /// <summary>
        /// Configurations for CORS rules
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            // TODO: Change to be more strict
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

        /// <summary>
        /// Configurations for Swagger (OpenAPI)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            // Read more https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                // TODO: These settings can be changed more specific if needed
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
                // This enables the <summary></summary> XML comments on Controller methods to be included in Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        #endregion

        #region Configure Development Settings

        /// <summary>
        /// Configurations for all the settings that are used when running on development mode
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDevelopmentSettings(this IServiceCollection services)
        {
            // TODO: Environment checking here
            ConfigureListStartupServices(services);
            services.AddDatabaseDeveloperPageExceptionFilter(); // https://github.com/aspnet/Announcements/issues/432
            return services;
        }

        /// <summary>
        /// Configuration for handy little service listing
        /// Makes it easy to see list of registered services
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureListStartupServices(IServiceCollection services)
        {
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);
                config.Path = "/listservices"; 
            });
        }

        #endregion

        #region Configure Settings

        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));

            return services;
        }

        #endregion

        #region Configure HealthChecks

        /// <summary>
        /// Configurations for all the health checks
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
            // TODO: Other health checks are not yet updated to .NET 5.0. Enable when those are NuGets are updated.
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

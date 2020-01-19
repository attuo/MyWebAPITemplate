using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApiTemplate.Extensions
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
    }
}

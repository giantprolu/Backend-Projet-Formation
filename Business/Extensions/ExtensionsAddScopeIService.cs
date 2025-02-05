using Business.ServicesMinimal;
using Microsoft.Extensions.DependencyInjection;
using Models.Repository;
using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;
using Models.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace Business.Extensions

{
    public static class ExtensionsAddScopeIService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddApplicationRepo();
            services.AddDbContextMini();
            services.AddScoped<IEleveServiceMini, EleveServiceMini>();
            services.AddScoped<ISchoolServiceMini, SchoolServiceMini>();

            return services;
        }

        public static IServiceCollection UseSqlServe(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSqlServerContext(config);
            return services;
        }

        public static IServiceCollection AppDbContextMini(this IServiceCollection services)
        {
            services.AddDbContextMini();
            return services;
        }

        public static IServiceCollection AddHostedService(this IServiceCollection services)
        {
            services.AddHostedService<HostedService>();
            return services;
        }
    }
}
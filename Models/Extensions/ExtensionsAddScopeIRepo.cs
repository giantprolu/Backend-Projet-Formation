using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Models.ModelMinimal;
using Models.Repository;
using Models.RepositorySchool;

namespace Models.Extensions
{
    public static class ExtensionsAddScopeIRepo
    {
        public static IServiceCollection AddApplicationRepo(this IServiceCollection services)
        {
            services.AddScoped<IEleveRepo, EleveRepo>();
            services.AddScoped<ISchoolRepo, SchoolRepo>();

            //services.AddDbContext<IEleveContextMini, EleveContextMini>();

            return services;
        }

        public static IServiceCollection AddDbContextMini(this IServiceCollection services)
        {
            services.AddScoped<IEleveContextMini>(service => service.GetRequiredService<EleveContextMini>());
            return services;
        }
    }
}
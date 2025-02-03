using Business.ServicesMinimal;
using Microsoft.Extensions.DependencyInjection;
using Models.Repository;
using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;
using Models.Extensions;

namespace Business.Extensions

{
    public static class ExtensionsAddScopeIService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddApplicationRepo();
            services.AddScoped<IEleveServiceMini, EleveServiceMini>();
            services.AddScoped<ISchoolServiceMini, SchoolServiceMini>();

            return services;
        }
    }
}
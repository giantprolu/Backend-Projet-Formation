using Business.ServicesMinimal;
using Microsoft.Extensions.DependencyInjection;
using Models.Repository;
using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;

namespace Business.Extensions

{
    public static class ExtensionsAddScope
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEleveServiceMini, EleveServiceMini>();
            services.AddScoped<ISchoolServiceMini, SchoolServiceMini>();
            services.AddScoped(typeof(IEleveRepo<>), typeof(EleveRepo<>));
            return services;
        }
    }
}
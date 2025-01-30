using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.ModelMinimal;

namespace Business.Extensions
{
    public static class ExtensionSqlServer
    {
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<EleveContextMini>
           (opt => opt.UseSqlServer(connectionString));
        }
    }
}
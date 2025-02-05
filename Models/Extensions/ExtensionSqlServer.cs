using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Models.ModelMinimal;

namespace Models.Extensions
{
    public static class ExtensionSqlServer
    {
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<EleveContextMini>
           (opt => opt.UseSqlServer(connectionString));
        }
    }
}
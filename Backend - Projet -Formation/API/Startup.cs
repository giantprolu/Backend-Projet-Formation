using Models.ModelAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Business.ServicesAPI;

internal class Startup
{
    // Propri�t� pour acc�der � la configuration de l'application
    public IConfiguration Configuration { get; }

    // Constructeur qui initialise la configuration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // M�thode pour configurer les services de l'application
    public void ConfigureServices(IServiceCollection services)
    {
        // Ajouter le contexte de la base de donn�es en utilisant SQL Server
        services.AddDbContextPool<EleveContextAPI>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Ajouter les contr�leurs
        services.AddControllers();
        services.AddScoped<IEleveServiceAPI, EleveServiceAPI>();

        // Ajouter Swagger pour la documentation de l'API
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }

    // M�thode pour configurer le pipeline de traitement des requ�tes HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Si l'environnement est en d�veloppement, utiliser la page d'exception du d�veloppeur
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Ajouter le middleware de routage
        app.UseRouting();

        // Ajouter le middleware d'autorisation
        app.UseAuthorization();

        // Configurer Swagger pour g�n�rer et servir la documentation de l'API
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Swagger sera accessible � la racine
        });

        // Configurer les points de terminaison pour les contr�leurs
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

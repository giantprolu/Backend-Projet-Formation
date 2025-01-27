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
    // Propriété pour accéder à la configuration de l'application
    public IConfiguration Configuration { get; }

    // Constructeur qui initialise la configuration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Méthode pour configurer les services de l'application
    public void ConfigureServices(IServiceCollection services)
    {
        // Ajouter le contexte de la base de données en utilisant SQL Server
        services.AddDbContextPool<EleveContextAPI>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Ajouter les contrôleurs
        services.AddControllers();
        services.AddScoped<IEleveServiceAPI, EleveServiceAPI>();

        // Ajouter Swagger pour la documentation de l'API
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }

    // Méthode pour configurer le pipeline de traitement des requêtes HTTP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Si l'environnement est en développement, utiliser la page d'exception du développeur
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Ajouter le middleware de routage
        app.UseRouting();

        // Ajouter le middleware d'autorisation
        app.UseAuthorization();

        // Configurer Swagger pour générer et servir la documentation de l'API
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Swagger sera accessible à la racine
        });

        // Configurer les points de terminaison pour les contrôleurs
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

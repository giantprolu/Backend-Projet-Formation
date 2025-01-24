// Importation des biblioth?ques n?cessaires
using Microsoft.EntityFrameworkCore;
using Models.ModelAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Business.ServicesAPI;



// Cr?ation de l'application web avec les arguments pass?s en ligne de commande
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(8000); // HTTP port
    options.ListenLocalhost(8001, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS port
    });
});
// Configuration des services CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    // Ajout d'une politique CORS nomm?e "AllowAngularOrigins"
    options.AddPolicy("AllowAngularOrigins",
            builder => builder.AllowAnyOrigin() // Permettre les requ?tes de n'importe quelle origine
                              .AllowAnyHeader() // Permettre n'importe quel en-t?te
                              .AllowAnyMethod()); // Permettre n'importe quelle m?thode HTTP (GET, POST, etc.)
});
builder.Services.AddScoped<IEleveServiceAPI, EleveServiceAPI>();
// Ajout des services de contr?leurs ? l'application
builder.Services.AddControllers();

// Configuration du contexte de base de donn?es avec Entity Framework Core
builder.Services.AddDbContext<EleveContextAPI>(opt =>
    // Utilisation de SQL Server avec la cha?ne de connexion d?finie dans le fichier de configuration
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajout des services pour explorer les points de terminaison de l'API
builder.Services.AddEndpointsApiExplorer();

// Ajout des services Swagger pour la documentation de l'API
builder.Services.AddSwaggerGen();

// Construction de l'application web
var app = builder.Build();

// Configuration conditionnelle pour l'environnement de d?veloppement
if (app.Environment.IsDevelopment())
{
    // Utilisation de Swagger pour g?n?rer et afficher la documentation de l'API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirection des requ?tes HTTP vers HTTPS
// app.UseHttpsRedirection();

// Activation de la politique CORS d?finie pr?c?demment
app.UseCors("AllowAngularOrigins");

// Activation de l'autorisation (sans sp?cifier de politique d'autorisation particuli?re)
app.UseAuthorization();

// Mappage des contr?leurs pour g?rer les requ?tes entrantes
app.MapControllers();

// D?marrage de l'application web
app.Run();

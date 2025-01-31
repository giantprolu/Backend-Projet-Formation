using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Business.ServicesMinimal;
using System.Reflection;
using Business.Extensions;
using Models.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure SQL Server context
        builder.Services.ConfigureSqlServerContext(builder.Configuration);

        // Add database developer page exception filter
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularOrigins",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });

        // Add controllers with JSON options
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        // Add Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
            c.CustomSchemaIds(type => type.FullName);
        });

        // Add application services and repositories
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationRepo();

        // Build the application
        WebApplication? app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
            });
        }

        // Map routes
        var interfaceType = typeof(IAddRoute);
        var implementingTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in implementingTypes)
        {
            var route = Activator.CreateInstance(type) as IAddRoute;
            route?.MapRoutes(app);
        }

        // Use middleware
        app.UseHttpsRedirection();
        app.UseCors("AllowAngularOrigins");
        app.UseAuthorization();
        app.UseRouting();

        // Run the application
        app.Run();
    }
}
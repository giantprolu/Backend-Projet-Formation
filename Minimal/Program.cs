using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Business.ServicesMinimal;
using System.Reflection;
using Business.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //builder.Services.AddScoped(typeof(IEleveRepo<>), typeof(EleveRepo<>));
        //builder.Services.AddDbContext<EleveContextMini>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
        builder.Services.ConfigureSqlServerContext(builder.Configuration);
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularOrigins",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
            c.CustomSchemaIds(type => type.FullName);
        });

        //builder.Services.AddScoped<IEleveServiceMini, EleveServiceMini>();
        builder.Services.AddApplicationServices();
        //builder.Services.AddScoped<ISchoolServiceMini, SchoolServiceMini>();
        builder.Services.AddLogging();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
            });
        }

        var interfaceType = typeof(IAddRoute);
        var implementingTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in implementingTypes)
        {
            var route = Activator.CreateInstance(type) as IAddRoute;
            route?.MapRoutes(app);
        }

        //var routeBuilderEleve = new EleveRoute();
        //routeBuilderEleve.AddRoutes(app);

        //var routeBuilderSchool = new SchoolRoute();
        //routeBuilderSchool.AddRoutes(app);

        app.UseHttpsRedirection();
        app.UseCors("AllowAngularOrigins");
        app.UseAuthorization();
        app.UseRouting();

        app.Run();
    }
}
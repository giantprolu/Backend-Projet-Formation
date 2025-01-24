using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Business.ServicesMinimal;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EleveContextMini>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Configuration des services CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    // Ajout d'une politique CORS nommée "AllowAngularOrigins"
    options.AddPolicy("AllowAngularOrigins",
            builder => builder.WithOrigins("http://localhost:4200") // Permettre les requêtes de n'importe quelle origine
                              .AllowAnyHeader() // Permettre n'importe quel en-tête
                              .AllowAnyMethod()); // Permettre n'importe quelle méthode HTTP (GET, POST, etc.)
});
// Configurer le sérialiseur JSON pour gérer les cycles de référence
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckble
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddScoped<IEleveServiceMini, EleveServiceMini>();
builder.Services.AddScoped<ISchoolServiceMini, SchoolServiceMini>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularOrigins");
app.UseAuthorization();

app.MapGet("/ListEleve", HandleGetListEleveAsync)
    .Produces<List<Eleve>>()
    .Produces(StatusCodes.Status204NoContent);

app.MapGet("/eleve/{id}", HandleGetEleveByIdAsync)
    .Produces<Eleve>()
    .Produces(StatusCodes.Status404NotFound);

app.MapPost("/eleve", HandlePostEleveAsync)
    .Produces<Eleve>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status404NotFound);

app.MapDelete("/eleve/{id}", HandleDeleteEleveAsync)
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

app.MapPut("/eleve/updateByName/{nom}", HandleUpdateEleveByNameAsync)
    .Produces<Eleve>()
    .Produces(StatusCodes.Status404NotFound);

app.MapGet("/ListSchools", HandleGetListSchoolsAsync)
    .Produces<List<School>>()
    .Produces(StatusCodes.Status204NoContent);

async Task<IResult> HandleGetListEleveAsync(IEleveServiceMini service)
{
    var eleves = await service.GetListEleveAsync();
    return eleves.Count > 0 ? Results.Ok(eleves) : Results.NoContent();
}

async Task<IResult> HandleGetEleveByIdAsync(IEleveServiceMini service, int id)
{
    var eleve = await service.GetEleveByIdAsync(id);
    return eleve is not null ? Results.Ok(eleve) : Results.NotFound($"Élève avec l'ID {id} non trouvé.");
}

async Task<IResult> HandlePostEleveAsync(IEleveServiceMini service, Models.ModelMinimal.EleveMini eleve, string schoolName)
{
    var result = await service.PostEleveAsync(eleve, schoolName);
    return result switch
    {
        not null => Results.Created($"/eleve/{result.Id}", result),
        _ => Results.NotFound("École non trouvée.")
    };
}


async Task<IResult> HandleDeleteEleveAsync(IEleveServiceMini service, int id)
{
    var success = await service.DeleteEleveAsync(id);
    return success ? Results.NoContent() : Results.NotFound($"Élève avec l'ID {id} non trouvé.");
}

async Task<IResult> HandleUpdateEleveByNameAsync(IEleveServiceMini   service, string nom, Models.ModelMinimal.EleveMini updatedEleve, string newSchoolName)
{
    var eleve = await service.UpdateEleveByNameAsync(nom, updatedEleve, newSchoolName);
    return eleve is not null ? Results.Ok(eleve) : Results.NotFound($"Élève avec le nom {nom} non trouvé ou école {newSchoolName} non trouvée.");
}

async Task<IResult> HandleGetListSchoolsAsync(ISchoolServiceMini service)
{
    var schools = await service.GetListSchoolsAsync();
    return schools.Count > 0 ? Results.Ok(schools) : Results.NoContent();
}

app.Run();


public record Eleve
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public int Age { get; set; }
    public required string Sexe { get; set; }
    public int SchoolId { get; set; }
    public required School Schools { get; set; }
}

public record School
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public int NmbEleve { get; set; }
}
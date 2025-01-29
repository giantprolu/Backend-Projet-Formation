using Business.ServicesMinimal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Models.ModelMinimal;

namespace Minimal.Class
{
    public class EleveRoute : IAddRoute
    {
        public void MapRoutes(WebApplication app)
        {
            
            var eleveGroup = app.MapGroup("/eleve");
            var eleveidGroup = eleveGroup.MapGroup("/{id}");
            
            app.MapGet("/ListEleve", HandleGetListEleveAsync)
                .Produces<IEnumerable<EleveMini>>()
                .Produces(StatusCodes.Status204NoContent);

            eleveidGroup.MapGet(string.Empty, HandleGetEleveByIdAsync)
                .Produces<EleveMini>()
                .Produces(StatusCodes.Status404NotFound);

            eleveGroup.MapPost(string.Empty, HandlePostEleveAsync)
                .Produces<EleveMini>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound);

            eleveidGroup.MapDelete(string.Empty, HandleDeleteEleveAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            eleveGroup.MapPut("/updateById/{id}", HandleUpdateEleveByIdAsync)
                .Produces<EleveMini>()
                .Produces(StatusCodes.Status404NotFound);
        }
        static async Task<IResult> HandleGetListEleveAsync(IEleveServiceMini service)
        {
            var eleves = await service.GetListEleveAsync().ConfigureAwait(false);
            return eleves.Count > 0 ? Results.Ok(eleves) : Results.NoContent();
        }

        static async Task<IResult> HandleGetEleveByIdAsync(IEleveServiceMini service, int id)
        {
            var eleve = await service.GetEleveByIdAsync(id);
            return eleve is not null ? Results.Ok(eleve) : Results.NotFound($"Élève avec l'ID {id} non trouvé.");
        }

        static async Task<IResult> HandlePostEleveAsync(IEleveServiceMini service, Models.ModelMinimal.EleveMini eleve, string schoolName)
        {
            var result = await service.PostEleveAsync(eleve, schoolName);
            return result switch
            {
                not null => Results.Created($"/eleve/{result.Id}", result),
                _ => Results.NotFound("École non trouvée.")
            };
        }

        static async Task<IResult> HandleDeleteEleveAsync(IEleveServiceMini service, int id)
        {
            var success = await service.DeleteEleveAsync(id);
            return success ? Results.NoContent() : Results.NotFound($"Élève avec l'ID {id} non trouvé.");
        }

        static async Task<IResult> HandleUpdateEleveByIdAsync(IEleveServiceMini service, int Id, string newNom, string newPrenom, int newAge, bool newSexe, string newSchoolName)
        {
            var updatedEleve = new EleveMini
            {
                Nom = newNom,
                Prenom = newPrenom,
                Age = newAge,
                Sexe = newSexe
            };

            var eleve = await service.UpdateEleveByIdAsync(Id, updatedEleve, newSchoolName);
            return eleve is not null ? Results.Ok(eleve) : Results.NotFound($"Élève avec l'ID {Id} non trouvé ou école {newSchoolName} non trouvée.");
        }

        
    }

}

using Business.ServicesMinimal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Business.Models;

namespace Minimal.Class
{
    public class EleveRoute : IAddRoute
    {
        public void MapRoutes(WebApplication app)
        {
            
            var eleveGroup = app.MapGroup("/eleve");
            var eleveidGroup = eleveGroup.MapGroup("/{id}");
            
            app.MapGet("/ListEleve", HandleGetListEleveAsync)
                .Produces<IEnumerable<Eleve>>()
                .Produces(StatusCodes.Status204NoContent);

            eleveidGroup.MapGet(string.Empty, HandleGetEleveByIdAsync)
                .Produces<Eleve>()
                .Produces(StatusCodes.Status404NotFound);

            eleveGroup.MapPost(string.Empty, HandlePostEleveAsync)
                .Produces<Eleve>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound);

            eleveidGroup.MapDelete(string.Empty, HandleDeleteEleveAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            eleveGroup.MapPut("/updateById/{id}", HandleUpdateEleveByIdAsync)
                .Produces<Eleve>()
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

        static async Task<IResult> HandlePostEleveAsync(IEleveServiceMini service, Models.ModelMinimal.EleveMini eleve)
        {
            var result = await service.PostEleveAsync(eleve);
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

        static async Task<IResult> HandleUpdateEleveByIdAsync(IEleveServiceMini service, int id, Eleve updatedEleve)
        {
            var eleve = await service.UpdateEleveByIdAsync(id, updatedEleve);
            return eleve is not null ? Results.Ok(eleve) : Results.NotFound($"Élève avec l'ID {id} non trouvé.");
        }


    }

}

using Business.ServicesMinimal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Models.ModelMinimal;

namespace Minimal.Class
{
    public class SchoolRoute : IAddRoute
    {
        public void MapRoutes(WebApplication app)
        {
            
            
            app.MapGet("/ListSchools", HandleGetListSchoolsAsync)
                .Produces<List<SchoolMini>>()
                .Produces(StatusCodes.Status204NoContent);
        }
        static async Task<IResult> HandleGetListSchoolsAsync(ISchoolServiceMini service)
        {
            var schools = await service.GetListSchoolsAsync();
            return schools.Count > 0 ? Results.Ok(schools) : Results.NoContent();
        }
        
    }
}

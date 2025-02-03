using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.ModelMinimal;

namespace Models.Repository
{
    public interface IEleveRepo : IRepository<EleveMini>
    {
        Task<List<EleveMini>> GetListEleveAsync();

        Task<EleveMini?> GetEleveByIdAsync(int id);

        Task<EleveMini?> PostEleveAsync(EleveMini eleve);

        Task<bool> DeleteEleveAsync(int id);

        Task<EleveMini?> UpdateEleveByIdAsync(int id, EleveMini eleve);

        IQueryable<EleveMini> FindEleveAsync(Expression<Func<EleveMini, bool>>? predicate = null,
            Expression<Func<EleveMini, IProperty>>? navigationPropertyPath = null,
            bool asNoTracking = true);
    }
}
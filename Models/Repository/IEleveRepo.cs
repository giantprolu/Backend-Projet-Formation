using System.Linq.Expressions;
using Models.ModelMinimal;

namespace Models.Repository
{
    public interface IEleveRepo<T> : IRepository<EleveMini>
    {
        Task<List<T>> GetListEleveAsync();

        Task<T?> GetEleveByIdAsync(int id);

        Task<T?> PostEleveAsync(T eleve);

        Task<bool> DeleteEleveAsync(int id);

        Task<T?> UpdateEleveByIdAsync(int id, T eleve);
    }
}
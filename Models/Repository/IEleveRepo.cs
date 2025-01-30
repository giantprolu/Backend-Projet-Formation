using System.Linq.Expressions;

namespace Models.Repository
{
    public interface IEleveRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetListEleveAsync();

        Task<T?> GetEleveByIdAsync(int id);

        Task PostEleveAsync(T entity);

        Task UpdateEleveByIdAsync(T entity);

        Task DeleteEleveAsync(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
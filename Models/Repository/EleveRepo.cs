using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;

namespace Models.Repository
{
    public class EleveRepo<T> : IEleveRepo<T> where T : class
    {
        protected EleveContextMini _context { get; set; }

        public EleveRepo(EleveContextMini context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetListEleveAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetEleveByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task PostEleveAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEleveByIdAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEleveAsync(int id)
        {
            var entity = await GetEleveByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
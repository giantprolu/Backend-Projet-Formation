using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;

namespace Models.Repository
{
    internal class EleveRepo : Repository<EleveMini>, IEleveRepo<EleveMini>
    {
        private readonly EleveContextMini _context;

        public EleveRepo(DbContextOptions<EleveContextMini> options)
        {
            _context = new EleveContextMini(options);
        }

        public async Task<bool> DeleteEleveAsync(int id)
        {
            var entity = await GetEleveByIdAsync(id);
            if (entity != null)
            {
                _context.Set<EleveMini>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<EleveMini?> GetEleveByIdAsync(int id)
        {
            return await _context.Set<EleveMini>().FindAsync(id);
        }

        public async Task<List<EleveMini>> GetListEleveAsync()
        {
            return await _context.Set<EleveMini>().ToListAsync();
        }

        public async Task<EleveMini?> PostEleveAsync(EleveMini eleve)
        {
            await _context.Set<EleveMini>().AddAsync(eleve);
            await _context.SaveChangesAsync();
            return eleve;
        }

        public async Task<EleveMini?> UpdateEleveByIdAsync(int id, EleveMini eleve)
        {
            _context.Set<EleveMini>().Update(eleve);
            await _context.SaveChangesAsync();
            return eleve;
        }

        public async Task<IEnumerable<EleveMini>> FindEleveAsync(Expression<Func<EleveMini, bool>> predicate)
        {
            return await _context.Set<EleveMini>().Where(predicate).ToListAsync();
        }
    }
}
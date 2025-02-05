using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Models.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models.Repository
{
    internal class EleveRepo : Repository<EleveMini>, IEleveRepo
    {
        private readonly EleveContextMini _context;
        private DbSet<EleveMini> globalVar;

        public EleveRepo(DbContextOptions<EleveContextMini> options)
        {
            _context = new EleveContextMini(options);
            globalVar = _context.Set<EleveMini>();
        }

        public async Task<bool> DeleteEleveAsync(int id)
        {
            var entity = await GetEleveByIdAsync(id).ConfigureAwait(false);
            if (entity != null)
            {
                globalVar.Remove(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return false;
        }

        public async Task<EleveMini?> GetEleveByIdAsync(int id)
        {
            return await globalVar
                         .Include(e => e.Schools)
                         .FirstOrDefaultAsync(e => e.Id == id)
                         .ConfigureAwait(false);
        }

        public async Task<List<EleveMini>> GetListEleveAsync()
        {
            return await globalVar.NoApplyTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<EleveMini?> PostEleveAsync(EleveMini eleve)
        {
            var school = await _context.Schools.FirstOrDefaultAsync().ConfigureAwait(false);
            if (school is null)
            {
                return null;
            }

            eleve.SchoolId = school.Id;
            _context.Eleves.Add(eleve);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var createdEleveMini = await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == eleve.Id)
                .ConfigureAwait(false);
            return createdEleveMini;
        }

        public async Task<EleveMini?> UpdateEleveByIdAsync(int id, EleveMini updatedEleve)
        {
            var eleveMini = await _context.Eleves
                .FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            if (eleveMini is null)
            {
                return null;
            }

            eleveMini.Nom = updatedEleve.Nom;
            eleveMini.Prenom = updatedEleve.Prenom;
            eleveMini.Age = updatedEleve.Age;
            eleveMini.Sexe = updatedEleve.Sexe;
            eleveMini.SchoolId = updatedEleve.SchoolId;

            _context.Entry(eleveMini).State = EntityState.Modified;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return eleveMini;
        }

        public IQueryable<EleveMini> FindEleveAsync(
            Expression<Func<EleveMini, bool>>? predicate = null,
            Expression<Func<EleveMini, IProperty>>? navigationPropertyPath = null,
            bool asNoTracking = true)
        {
            var retour = globalVar.AsQueryable();
            if (navigationPropertyPath != null)
                retour = retour.Include(navigationPropertyPath);

            if (predicate != null)
                retour = retour.Where(predicate);

            return retour.NoApplyTracking(asNoTracking);
        }
    }
}
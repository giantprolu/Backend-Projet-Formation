using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;
using Business.Profile;
using Business.Models;
using Models.Repository;
using System.Linq.Expressions;

namespace Business.ServicesMinimal
{
    internal class EleveServiceMini : IEleveServiceMini
    {
        private readonly IEleveRepo<Eleve> _eleveRepo;
        private readonly IEleveContextMini _context;

        public EleveServiceMini(IEleveRepo<Eleve> eleveRepo, IEleveContextMini context)
        {
            _eleveRepo = eleveRepo;
            _context = context;
        }

        public async Task<List<Eleve>> GetListEleveAsync()
        {
            var eleve = await _eleveRepo.GetListEleveAsync();
            var eleves = await _context.Eleves.Include(e => e.Schools).AsNoTracking().ToListAsync();
            return eleves.Select(e => e.ToEleve()).ToList();
        }

        public async Task<Eleve?> GetEleveByIdAsync(int id)
        {
            var eleveMini = await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == id);
            return eleveMini?.ToEleve();
        }

        public async Task<Eleve?> PostEleveAsync(Eleve eleve)
        {
            var school = await _context.Schools.FirstOrDefaultAsync();
            if (school == null)
            {
                return null;
            }

            var eleveMini = eleve.ToEleveMini();
            eleveMini.SchoolId = school.Id;
            _context.Eleves.Add(eleveMini);
            await _context.SaveChangesAsync();

            var createdEleveMini = await _context.Eleves.Include(e => e.Schools).FirstOrDefaultAsync(e => e.Id == eleveMini.Id);
            return createdEleveMini?.ToEleve();
        }

        public async Task<bool> DeleteEleveAsync(int id)
        {
            var eleve = await _context.Eleves.FindAsync(id);
            if (eleve == null)
            {
                return false;
            }

            _context.Eleves.Remove(eleve);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Eleve?> UpdateEleveByIdAsync(int id, Eleve updatedEleve)
        {
            var eleveMini = await _context.Eleves.Include(e => e.Schools).FirstOrDefaultAsync(e => e.Id == id);
            if (eleveMini == null)
            {
                return null;
            }

            eleveMini.Nom = updatedEleve.Nom;
            eleveMini.Prenom = updatedEleve.Prenom;
            eleveMini.Age = updatedEleve.Age;
            eleveMini.Sexe = updatedEleve.Sexe;
            eleveMini.SchoolId = updatedEleve.SchoolId;

            _context.Entry(eleveMini).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return eleveMini.ToEleve();
        }

        public Task<IEnumerable<EleveMini>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EleveMini?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EleveMini?> PostAsync(EleveMini entity)
        {
            throw new NotImplementedException();
        }

        public Task<EleveMini?> UpdateByIdAsync(EleveMini entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EleveMini>> FindAsync(Expression<Func<EleveMini, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
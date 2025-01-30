using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;
using Business.Profile;
using Business.Models;
using Models.Repository;
using System.Linq.Expressions;

namespace Business.ServicesMinimal
{
    internal class EleveServiceMini : EleveRepo<Eleve>, IEleveServiceMini
    {
        public EleveServiceMini(EleveContextMini context) : base(context)

        {
        }

        public new async Task<List<Eleve>> GetListEleveAsync()
        {
            var elevesMini = await _context.Eleves.Include(e => e.Schools).AsNoTracking().ToListAsync();
            return elevesMini.Select(e => e.ToEleve()).ToList();
        }

        public new async Task<Eleve?> GetEleveByIdAsync(int id)
        {
            var eleveMini = await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == id);
            return eleveMini?.ToEleve();
        }

        public new async Task<Eleve?> PostEleveAsync(Eleve eleve)
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

        public new async Task<bool> DeleteEleveAsync(int id)
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
    }
}
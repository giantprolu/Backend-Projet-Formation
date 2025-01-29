using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;

namespace Business.ServicesMinimal
{
    public class EleveServiceMini : IEleveServiceMini
    {
        private readonly EleveContextMini _context;

        public EleveServiceMini(EleveContextMini context)
        {
            _context = context;
        }

        public async Task<List<EleveMini>> GetListEleveAsync()
        {
            return await _context.Eleves.Include(e => e.Schools).ToListAsync();
        }

        public async Task<EleveMini?> GetEleveByIdAsync(int id)
        {
            return await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EleveMini?> PostEleveAsync(EleveMini eleve)
        {
            var school = await _context.Schools.FirstOrDefaultAsync();
            if (school == null)
            {
                return null;
            }

            eleve.SchoolId = school.Id;
            _context.Eleves.Add(eleve);
            await _context.SaveChangesAsync();

            return await _context.Eleves.Include(e => e.Schools).FirstOrDefaultAsync(e => e.Id == eleve.Id);
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
        public async Task<EleveMini?> UpdateEleveByIdAsync(int id, EleveMini updatedEleve)
        {
            var eleve = await _context.Eleves.Include(e => e.Schools).FirstOrDefaultAsync(e => e.Id == id);
            if (eleve == null)
            {
                return null;
            }

            eleve.Nom = updatedEleve.Nom;
            eleve.Prenom = updatedEleve.Prenom;
            eleve.Age = updatedEleve.Age;
            eleve.Sexe = updatedEleve.Sexe;
            eleve.SchoolId = updatedEleve.SchoolId;
            eleve.Schools = updatedEleve.Schools;

            _context.Entry(eleve).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return eleve;
        }
    }
}

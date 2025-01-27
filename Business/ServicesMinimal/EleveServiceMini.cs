using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<EleveMini?> PostEleveAsync(EleveMini eleve, string schoolName)
        {
            var school = await _context.Schools.FirstOrDefaultAsync(s => s.Nom == schoolName);
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

        public async Task<EleveMini?> UpdateEleveByNameAsync(string nom, EleveMini updatedEleve, string newSchoolName)
        {
            var eleve = await _context.Eleves.Include(e => e.Schools).FirstOrDefaultAsync(e => e.Nom == nom);
            if (eleve == null)
            {
                return null;
            }

            // Mettre à jour les propriétés de l'élève
            eleve.Nom = updatedEleve.Nom;
            eleve.Prenom = updatedEleve.Prenom;
            eleve.Age = updatedEleve.Age;
            eleve.Sexe = updatedEleve.Sexe;

            // Vérifier si le nom de la nouvelle école existe dans la base de données
            var newSchool = await _context.Schools.FirstOrDefaultAsync(s => s.Nom == newSchoolName);
            if (newSchool == null)
            {
                return null;
            }

            // Mettre à jour l'école de l'élève
            eleve.SchoolId = newSchool.Id;
            eleve.Schools = newSchool;

            // Marquer l'entité comme modifiée
            _context.Entry(eleve).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return eleve;
        }

        public async Task<List<SchoolMini>> GetListSchoolsAsync()
        {
            return await _context.Schools.ToListAsync();
        }
    }
}

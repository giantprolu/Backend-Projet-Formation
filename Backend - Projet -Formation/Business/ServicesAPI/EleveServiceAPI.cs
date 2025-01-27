using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Models.ModelAPI;

namespace Business.ServicesAPI
{
    public class EleveServiceAPI : IEleveServiceAPI
    {
        private readonly EleveContextAPI _context;
        private readonly ILogger<EleveServiceAPI> _logger;

        public EleveServiceAPI(EleveContextAPI context, ILogger<EleveServiceAPI> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<EleveAPI>> GetElevesAsync()
        {
            _logger.LogInformation("Récupération de tous les étudiants de la base de données.");
            return await _context.Eleves.ToListAsync();
        }

        public async Task<EleveAPI> GetEleveByIdAsync(int id)
        {
            return await _context.Eleves.FindAsync(id) ?? throw new InvalidOperationException($"Élève avec l'ID {id} non trouvé.");
        }

        public async Task<EleveAPI> AddEleveAsync(EleveAPI eleve)
        {
            _context.Eleves.Add(eleve);
            await _context.SaveChangesAsync();
            return eleve;
        }

        public async Task<bool> UpdateEleveByNameAsync(string nom, EleveAPI updatedEleve)
        {
            var eleve = await _context.Eleves.FirstOrDefaultAsync(e => e.Nom == nom);
            if (eleve == null)
            {
                return false;
            }

            eleve.Nom = updatedEleve.Nom;
            eleve.Prenom = updatedEleve.Prenom;
            eleve.Age = updatedEleve.Age;
            eleve.Sexe = updatedEleve.Sexe;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EleveExists(eleve.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteEleveByIdAsync(int id)
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

        private bool EleveExists(int id)
        {
            return _context.Eleves.Any(e => e.Id == id);
        }
    }
}



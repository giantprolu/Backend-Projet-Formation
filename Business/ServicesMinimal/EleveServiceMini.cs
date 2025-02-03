using Models.ModelMinimal;
using Microsoft.EntityFrameworkCore;
using Business.Profile;
using Business.Models;
using Models.Repository;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Business.ServicesMinimal
{
    internal class EleveServiceMini : IEleveServiceMini
    {
        private readonly IEleveRepo _eleveRepo;

        public EleveServiceMini(IEleveRepo eleveRepo)
        {
            _eleveRepo = eleveRepo;
        }

        public async Task<List<Eleve>> GetListEleveAsync()
        {
            var eleves = await _eleveRepo.GetListEleveAsync();

            return eleves.Select(e => e.ToEleve()).ToList();
        }

        public async Task<Eleve?> GetEleveByIdAsync(int id)
        {
            var eleveMini = await _eleveRepo.GetEleveByIdAsync(id);
            return eleveMini?.ToEleve();
        }

        public async Task<Eleve?> PostEleveAsync(Eleve eleve)
        {
            var eleveMini = await _eleveRepo.PostEleveAsync(eleve.ToEleveMini());
            return eleve;
        }

        public async Task<bool> DeleteEleveAsync(int id)
        {
            var eleve = await _eleveRepo.GetEleveByIdAsync(id);
            return false;
        }

        public async Task<Eleve?> UpdateEleveByIdAsync(int id, Eleve updatedEleve)
        {
            var eleveMini = await _eleveRepo.UpdateEleveByIdAsync(id, updatedEleve.ToEleveMini());

            return eleveMini?.ToEleve();
        }

        public IQueryable<Eleve> FindEleveAsync(Expression<Func<Eleve, bool>>? predicate = null,
            Expression<Func<Eleve, IProperty>>? navigationPropertyPath = null,
            bool asNoTracking = true)
        {
            var eleve = _eleveRepo.FindEleveAsync();
            return eleve.Select(e => e.ToEleve());
        }
    }
}
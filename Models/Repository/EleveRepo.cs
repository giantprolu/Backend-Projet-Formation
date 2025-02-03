﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.ModelMinimal;
using Models.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var entity = await GetEleveByIdAsync(id);
            if (entity != null)
            {
                globalVar.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<EleveMini?> GetEleveByIdAsync(int id)
        {
            return await globalVar
                         .Include(e => e.Schools)
                         .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<EleveMini>> GetListEleveAsync()
        {
            return await globalVar.NoApplyTracking().ToListAsync();
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

            var createdEleveMini = await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == eleve.Id);
            return createdEleveMini;
        }

        public async Task<EleveMini?> UpdateEleveByIdAsync(int id, EleveMini updatedEleve)
        {
            var eleveMini = await _context.Eleves
                .Include(e => e.Schools)
                .FirstOrDefaultAsync(e => e.Id == id);
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

            if (asNoTracking != true)
                retour = retour.NoApplyTracking();
            return globalVar;
        }
    }
}
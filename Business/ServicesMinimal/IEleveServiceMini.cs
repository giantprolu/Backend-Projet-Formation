using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.ModelMinimal;
using Models.Repository;

namespace Business.ServicesMinimal
{
    public interface IEleveServiceMini
    {
        Task<List<Eleve>> GetListEleveAsync();

        Task<Eleve?> GetEleveByIdAsync(int id);

        Task<Eleve?> PostEleveAsync(Eleve eleve);

        Task<bool> DeleteEleveAsync(int id);

        Task<Eleve?> UpdateEleveByIdAsync(int id, Eleve eleve);

        IQueryable<Eleve> FindEleveAsync(Expression<Func<Eleve, bool>>? predicate = null,
            Expression<Func<Eleve, IProperty>>? navigationPropertyPath = null,
            bool asNoTracking = true);
    }
}
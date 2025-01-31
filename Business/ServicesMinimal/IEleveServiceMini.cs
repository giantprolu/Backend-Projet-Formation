using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Models.Repository;

namespace Business.ServicesMinimal
{
    public interface IEleveServiceMini : IEleveRepo<Eleve>
    {
        new Task<List<Eleve>> GetListEleveAsync();

        new Task<Eleve?> GetEleveByIdAsync(int id);

        new Task<Eleve?> PostEleveAsync(Eleve eleve);

        new Task<bool> DeleteEleveAsync(int id);

        new Task<Eleve?> UpdateEleveByIdAsync(int id, Eleve eleve);
    }
}
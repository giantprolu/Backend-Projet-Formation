using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;
using Business.Models;

namespace Business.ServicesMinimal
{
    public interface IEleveServiceMini
    {
        Task<List<Eleve>> GetListEleveAsync();
        Task<Eleve?> GetEleveByIdAsync(int id);
        Task<Eleve?> PostEleveAsync(Eleve eleve);
        Task<bool> DeleteEleveAsync(int id);
        Task<Eleve?> UpdateEleveByIdAsync(int id, Eleve updatedEleve);
    }
}

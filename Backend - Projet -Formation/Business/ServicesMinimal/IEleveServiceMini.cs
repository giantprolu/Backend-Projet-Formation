using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;

namespace Business.ServicesMinimal
{
    public interface IEleveServiceMini
    {
        Task<List<EleveMini>> GetListEleveAsync();
        Task<EleveMini?> GetEleveByIdAsync(int id);
        Task<EleveMini?> PostEleveAsync(EleveMini eleve, string schoolName);
        Task<bool> DeleteEleveAsync(int id);
        Task<EleveMini?> UpdateEleveByNameAsync(string nom, EleveMini updatedEleve, string newSchoolName);
    }
}

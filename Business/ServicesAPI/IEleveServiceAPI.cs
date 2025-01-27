using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelAPI;

namespace Business.ServicesAPI
{
    public interface IEleveServiceAPI
    {
        Task<IEnumerable<EleveAPI>> GetElevesAsync();
        Task<EleveAPI> GetEleveByIdAsync(int id);
        Task<EleveAPI> AddEleveAsync(EleveAPI eleve);
        Task<bool> UpdateEleveByNameAsync(string nom, EleveAPI updatedEleve);
        Task<bool> DeleteEleveByIdAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;


namespace Business.ServicesMinimal
{
    public interface ISchoolServiceMini
    {
        Task<List<SchoolMini>> GetListSchoolsAsync();
    }
}

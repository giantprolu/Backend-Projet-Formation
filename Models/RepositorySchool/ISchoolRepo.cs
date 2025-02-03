using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;

namespace Models.RepositorySchool
{
    public interface ISchoolRepo : IRepository<SchoolMini>
    {
        Task<List<SchoolMini>> GetListSchoolsAsync();
    }
}
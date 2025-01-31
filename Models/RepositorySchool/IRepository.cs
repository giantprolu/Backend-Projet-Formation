using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RepositorySchool
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
    }
}
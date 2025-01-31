using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RepositorySchool
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
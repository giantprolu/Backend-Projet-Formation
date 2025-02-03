using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.ModelMinimal;

namespace Models.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAsync(Expression<Func<T, bool>>? predicate = null,
            Expression<Func<T, IProperty>>? navigationPropertyPath = null,
            bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> PostAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T?> UpdateByIdAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models.ModelMinimal;

namespace Models.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T?> PostAsync(T entity);

        Task<T?> UpdateByIdAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
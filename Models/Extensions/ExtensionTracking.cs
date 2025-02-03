using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.Extensions
{
    public static class ExtensionTracking
    {
        public static IQueryable<T> NoApplyTracking<T>(this IQueryable<T> query, bool asNoTracking = true) where T : class
           => asNoTracking ? query.AsNoTracking() : query;
    }
}
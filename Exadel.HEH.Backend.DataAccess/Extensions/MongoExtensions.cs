using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Exadel.HEH.Backend.DataAccess.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoExtensions
    {
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable)
        {
            if (queryable is IMongoQueryable<T> mongoQueryable)
            {
                return IAsyncCursorSourceExtensions.ToListAsync(mongoQueryable);
            }

            return Task.FromResult(queryable.ToList());
        }

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> queryable,
            Expression<Func<T, bool>> predicate)
        {
            if (queryable is IMongoQueryable<T> mongoQueryable)
            {
                return IAsyncCursorSourceExtensions.FirstOrDefaultAsync(mongoQueryable.Where(predicate));
            }

            return Task.FromResult(queryable.FirstOrDefault(predicate));
        }
    }
}
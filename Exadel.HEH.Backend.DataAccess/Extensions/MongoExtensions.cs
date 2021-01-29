using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Exadel.HEH.Backend.DataAccess.Extensions
{
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
    }
}
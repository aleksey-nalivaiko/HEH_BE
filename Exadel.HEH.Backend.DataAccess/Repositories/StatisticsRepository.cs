using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class StatisticsRepository : MongoRepository<Statistics>, IStatisticsRepository
    {
        public StatisticsRepository(IDbContext context)
            : base(context)
        {
        }

        public Task UpdateIncrementAsync(Guid id, Expression<Func<Statistics, int>> field, int value)
        {
            return Context.UpdateIncrementAsync(id, field, value);
        }

        public Task<IEnumerable<Statistics>> GetInWhereAsync(
            Expression<Func<Statistics, Guid>> field,
            IEnumerable<Guid> discountIds,
            DateTime startDate,
            DateTime endDate)
        {
            if (startDate != default && endDate != default)
            {
                return Context.GetInAndWhereAsync(field, discountIds,
                    s => s.DateTime >= startDate && s.DateTime <= endDate);
            }

            return Context.GetInAndWhereAsync(field, discountIds);
        }
    }
}
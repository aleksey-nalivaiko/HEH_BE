using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IStatisticsRepository : IRepository<Statistics>
    {
        Task<bool> StatisticsExists(Expression<Func<Statistics, bool>> expression);

        Task UpdateIncrementAsync(Expression<Func<Statistics, bool>> expression,
            Expression<Func<Statistics, int>> field, int value);

        Task<IEnumerable<Statistics>> GetInWhereAsync(Expression<Func<Statistics, Guid>> field,
            IEnumerable<Guid> discountIds = default,
            DateTime startDate = default,
            DateTime endDate = default);

        Task RemoveAsync(Expression<Func<Statistics, bool>> expression);
    }
}
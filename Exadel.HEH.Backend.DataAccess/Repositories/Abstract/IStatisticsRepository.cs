using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    //TODO: Change to statistics
    public interface IStatisticsRepository : IRepository<Discount>
    {
        Task UpdateIncrementAsync(Guid id, Expression<Func<Discount, int>> field, int value);

        Task<int> SumAsync();
    }
}
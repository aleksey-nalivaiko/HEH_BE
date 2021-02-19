using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IQueryable<Discount> Get();

        Task<IEnumerable<Discount>> GetAsync(Expression<Func<Discount, bool>> expression);

        Task<IEnumerable<Discount>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task CreateManyAsync(IEnumerable<Discount> discounts);

        Task UpdateManyAsync(IEnumerable<Discount> discounts);

        Task RemoveAsync(Expression<Func<Discount, bool>> expression);
    }
}
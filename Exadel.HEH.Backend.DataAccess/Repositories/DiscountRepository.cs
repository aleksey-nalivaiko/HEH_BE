using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class DiscountRepository : MongoRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<Discount> Get()
        {
            return Context.GetAll<Discount>();
        }

        public Task<IEnumerable<Discount>> GetAsync(Expression<Func<Discount, bool>> expression)
        {
            return Context.GetAsync(expression);
        }

        public Task<IEnumerable<Discount>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return Context.GetAsync<Discount>(d => ids.Contains(d.Id));
        }

        public Task CreateManyAsync(IEnumerable<Discount> discounts)
        {
            return Context.CreateManyAsync(discounts);
        }

        public Task UpdateManyAsync(IEnumerable<Discount> discounts)
        {
            return Context.UpdateManyAsync(discounts);
        }

        public Task RemoveAsync(Expression<Func<Discount, bool>> expression)
        {
            return Context.RemoveAsync(expression);
        }

        public Task UpdateIncrementAsync(Guid id, Expression<Func<Discount, int>> field, int value)
        {
            return Context.UpdateIncrementAsync(id, field, value);
        }
    }
}
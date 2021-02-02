using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IQueryable<Discount> Get();

        Task<IEnumerable<Discount>> GetByIds(IEnumerable<Guid> ids);
    }
}
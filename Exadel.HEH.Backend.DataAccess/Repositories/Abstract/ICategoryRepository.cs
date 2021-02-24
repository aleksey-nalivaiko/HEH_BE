using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task<IEnumerable<Category>> GetAsync(Expression<Func<Category, bool>> expression);
    }
}
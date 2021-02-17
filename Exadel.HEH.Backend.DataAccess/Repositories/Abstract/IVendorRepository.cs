using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        IQueryable<Vendor> Get();

        Task UpdateIncrementAsync(Guid id, Expression<Func<Vendor, int>> field, int value);
    }
}
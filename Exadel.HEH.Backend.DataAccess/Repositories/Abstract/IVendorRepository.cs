using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Task UpdateIncrementAsync(Guid id, Expression<Func<Vendor, int>> field, int value);
    }
}
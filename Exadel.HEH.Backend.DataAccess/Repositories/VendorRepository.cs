using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class VendorRepository : MongoRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IDbContext context)
            : base(context)
        {
        }

        public Task UpdateIncrementAsync(Guid id, Expression<Func<Vendor, int>> field, int value)
        {
            return Context.UpdateIncrementAsync(id, field, value);
        }
    }
}
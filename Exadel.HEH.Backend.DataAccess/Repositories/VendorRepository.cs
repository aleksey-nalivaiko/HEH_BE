using System.Linq;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<Vendor> Get()
        {
            return Context.GetAll<Vendor>();
        }
    }
}
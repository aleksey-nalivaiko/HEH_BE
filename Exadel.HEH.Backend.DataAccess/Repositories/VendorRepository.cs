using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class VendorRepository : MongoRepository<Vendor>
    {
        public VendorRepository(IDbContext context)
            : base(context)
        {
        }
    }
}
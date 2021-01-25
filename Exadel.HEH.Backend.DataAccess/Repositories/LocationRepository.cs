using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class LocationRepository : MongoRepository<Location>
    {
        public LocationRepository(IDbContext context)
            : base(context)
        {
        }
    }
}

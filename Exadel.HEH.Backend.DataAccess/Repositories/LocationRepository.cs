using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<IEnumerable<Location>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return Context.GetAsync<Location>(location => ids.Contains(location.Id));
        }
    }
}

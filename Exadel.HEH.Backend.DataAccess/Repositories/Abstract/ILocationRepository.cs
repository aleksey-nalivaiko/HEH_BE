using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
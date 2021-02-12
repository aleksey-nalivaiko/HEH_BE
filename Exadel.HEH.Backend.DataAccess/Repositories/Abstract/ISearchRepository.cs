using System.Collections.Generic;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ISearchRepository : IRepository<Search>
    {
        IEnumerable<Search> Search();
    }
}
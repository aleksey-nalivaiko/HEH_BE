using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ISearchRepository : IRepository<Search>
    {
        Task<IEnumerable<Search>> SearchAsync(string path, string searchText);

        Task CreateManyAsync(IEnumerable<Search> searchList);

        Task RemoveAll();
    }
}
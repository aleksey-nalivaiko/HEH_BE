using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class SearchRepository : MongoRepository<Search>, ISearchRepository
    {
        public SearchRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<IEnumerable<Search>> SearchAsync(string path, string searchText)
        {
            return Context.SearchAsync<Search>(path, searchText);
        }
    }
}
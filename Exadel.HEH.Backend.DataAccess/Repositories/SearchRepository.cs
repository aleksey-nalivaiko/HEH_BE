using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class SearchRepository<TSearchDocument> : MongoRepository<TSearchDocument>,
        ISearchRepository<TSearchDocument>
        where TSearchDocument : class, IDataModel, new()
    {
        public SearchRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<IEnumerable<TSearchDocument>> SearchAsync(string path, string searchText)
        {
            return Context.SearchAsync<TSearchDocument>(path, searchText);
        }

        public Task CreateManyAsync(IEnumerable<TSearchDocument> searchList)
        {
            return Context.CreateManyAsync(searchList);
        }

        public Task RemoveAllAsync()
        {
            return Context.RemoveAllAsync<TSearchDocument>();
        }
    }
}
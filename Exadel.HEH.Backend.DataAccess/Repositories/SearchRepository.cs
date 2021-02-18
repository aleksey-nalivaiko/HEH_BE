using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class SearchRepository<TDocument> : MongoRepository<TDocument>,
        ISearchRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        public SearchRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<TDocument> Get()
        {
            return Context.GetAll<TDocument>();
        }

        public Task<IEnumerable<TDocument>> SearchAsync(string path, string searchText)
        {
            return Context.SearchAsync<TDocument>(path, searchText);
        }

        public Task CreateManyAsync(IEnumerable<TDocument> searchList)
        {
            return Context.CreateManyAsync(searchList);
        }

        public Task RemoveAllAsync()
        {
            return Context.RemoveAllAsync<TDocument>();
        }
    }
}
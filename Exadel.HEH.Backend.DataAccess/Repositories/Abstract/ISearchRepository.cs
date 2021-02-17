using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ISearchRepository<TDocument> : IRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        IQueryable<TDocument> Get();

        Task<IEnumerable<TDocument>> SearchAsync(string path, string searchText);

        Task CreateManyAsync(IEnumerable<TDocument> searchList);

        Task RemoveAllAsync();
    }
}
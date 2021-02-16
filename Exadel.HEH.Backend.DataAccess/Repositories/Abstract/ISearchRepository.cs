using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ISearchRepository<TSearchDocument> : IRepository<TSearchDocument>
        where TSearchDocument : class, IDataModel, new()
    {
        Task<IEnumerable<TSearchDocument>> SearchAsync(string path, string searchText);

        Task CreateManyAsync(IEnumerable<TSearchDocument> searchList);

        Task RemoveAllAsync();
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService<T, in TCreateUpdate>
    {
        Task<IQueryable<T>> SearchAsync(string searchText = default);

        Task CreateAsync(TCreateUpdate item);

        Task UpdateAsync(TCreateUpdate item);

        Task RemoveAsync(Guid id);

        Task ReindexAsync();
    }
}
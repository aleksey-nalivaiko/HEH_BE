using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService<T, in TCreateUpdate>
    {
        Task<IEnumerable<T>> SearchAsync(string searchText = default);

        Task CreateAsync(TCreateUpdate item);

        Task UpdateAsync(TCreateUpdate item);

        Task RemoveAsync(Guid id);

        Task ReindexAsync();
    }
}
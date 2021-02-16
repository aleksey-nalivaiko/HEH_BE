using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService<T, TDto>
    {
        IQueryable<T> Search(IQueryable<T> allItems, string searchText);

        Task CreateAsync(TDto item);

        Task UpdateAsync(TDto item);

        Task RemoveAsync(Guid id);

        Task ReindexAsync();
    }
}
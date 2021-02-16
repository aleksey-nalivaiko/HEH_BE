using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService
    {
        IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText);

        Task CreateAsync(Discount discount);

        Task UpdateAsync(Discount discountShort);

        Task RemoveAsync(Guid id);

        Task Reindex();
    }
}
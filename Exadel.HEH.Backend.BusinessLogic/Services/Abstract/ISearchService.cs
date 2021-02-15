using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService
    {
        IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText);

        Task CreateAsync(DiscountDto discount);

        Task UpdateAsync(DiscountDto discount);

        Task RemoveAsync(Guid id);

        Task Reindex();
    }
}
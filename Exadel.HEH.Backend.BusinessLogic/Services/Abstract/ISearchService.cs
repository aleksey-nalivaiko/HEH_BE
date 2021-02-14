using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISearchService
    {
        Task CreateAsync(DiscountDto discount);

        Task UpdateAsync(DiscountDto discount);

        Task RemoveAsync(Guid id);
    }
}
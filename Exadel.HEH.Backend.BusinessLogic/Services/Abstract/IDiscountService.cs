using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountService
    {
        IQueryable<DiscountDto> Get(string searchText);

        Task<DiscountDto> GetByIdAsync(Guid id);
    }
}
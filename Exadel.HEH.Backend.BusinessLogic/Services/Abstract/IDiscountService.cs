using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountService
    {
        Task<IQueryable<DiscountDto>> GetAsync(string searchText);

        Task<DiscountDto> GetByIdAsync(Guid id);
    }
}
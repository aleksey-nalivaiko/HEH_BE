using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountService
    {
        IQueryable<DiscountDto> Get();
    }
}
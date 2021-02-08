using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IDiscountValidationService
    {
        Task<bool> DiscountExists(Guid discountId);
    }
}
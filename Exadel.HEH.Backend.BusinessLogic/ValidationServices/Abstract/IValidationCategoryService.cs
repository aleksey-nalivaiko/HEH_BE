using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IValidationCategoryService
    {
        Task<bool> DiscountContainsCategory(Guid id);
    }
}

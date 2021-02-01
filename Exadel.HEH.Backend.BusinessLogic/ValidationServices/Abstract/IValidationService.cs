using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IValidationService
    {
        Task<bool> CheckOnDiscountContainsCategory(Guid id);
    }
}

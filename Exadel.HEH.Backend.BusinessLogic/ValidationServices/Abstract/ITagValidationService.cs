using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface ITagValidationService
    {
        Task<bool> DiscountContainsTag(Guid id, CancellationToken token = default);
    }
}

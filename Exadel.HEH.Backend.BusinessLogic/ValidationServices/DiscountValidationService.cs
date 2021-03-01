using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class DiscountValidationService : IDiscountValidationService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountValidationService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> DiscountExists(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return result != null;
        }

        public async Task<bool> DiscountNotExists(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return result is null;
        }

        public bool EndDateLaterThanStartDate(DiscountShortDto discount)
        {
            if (discount.EndDate != null)
            {
                return discount.EndDate.Value.Date >= discount.StartDate.Date;
            }

            return true;
        }
    }
}
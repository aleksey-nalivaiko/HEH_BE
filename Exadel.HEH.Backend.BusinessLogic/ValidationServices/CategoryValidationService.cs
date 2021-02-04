using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class CategoryValidationService : ICategoryValidationService
    {
        private readonly IDiscountRepository _discountRepository;

        public CategoryValidationService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> DiscountContainsCategory(Guid id, CancellationToken token)
        {
            var discounts = await _discountRepository.GetAllAsync();
            var anyDiscountWithCategory = false;

            foreach (var discount in discounts)
            {
                if (discount.CategoryId == id)
                {
                    anyDiscountWithCategory = true;
                    break;
                }
            }

            return anyDiscountWithCategory;
        }
    }
}

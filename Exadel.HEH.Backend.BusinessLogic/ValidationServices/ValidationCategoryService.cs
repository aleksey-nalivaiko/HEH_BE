using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class ValidationCategoryService : IValidationCategoryService
    {
        private readonly IDiscountRepository _discountRepository;

        public ValidationCategoryService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> CheckOnDiscountContainsCategory(Guid id)
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

using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class CategoryValidationService : ICategoryValidationService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDiscountRepository _discountRepository;

        public CategoryValidationService(ICategoryRepository categoryRepository,
            IDiscountRepository discountRepository)
        {
            _categoryRepository = categoryRepository;
            _discountRepository = discountRepository;
        }

        public async Task<bool> CategoryNotInDiscountsAsync(Guid id)
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

            return !anyDiscountWithCategory;
        }

        public async Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken token = default)
        {
            return await _categoryRepository.GetByIdAsync(categoryId) != null;
        }

        public async Task<bool> CategoryNotExistsAsync(Guid categoryId, CancellationToken token = default)
        {
            return await _categoryRepository.GetByIdAsync(categoryId) is null;
        }
    }
}

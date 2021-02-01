using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class ValidationService : IValidationService
    {
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<Category> _categoryRepository;
        private bool answer;

        public ValidationService(IRepository<Discount> discountRepository,
            IRepository<Category> categoryRepository)
        {
            _discountRepository = discountRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CheckOnDiscountContainsCategory(Guid id)
        {
            var discountCollention = await _discountRepository.GetAllAsync();
            var category = _categoryRepository.GetByIdAsync(id);
            foreach (var item in discountCollention)
            {
                answer = item.CategoryId.Equals(id);
            }

            return !answer;
        }
    }
}

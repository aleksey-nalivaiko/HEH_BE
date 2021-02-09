using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class TagValidationSevice : ITagValidationService
    {
        private readonly IDiscountRepository _discountRepository;

        public TagValidationSevice(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> DiscountContainsTag(Guid id, CancellationToken token)
        {
            var discounts = await _discountRepository.GetAllAsync();
            var anyDiscountWithCategory = false;

            foreach (var discount in discounts)
            {
                foreach (var item in discount.TagsIds)
                {
                    if (item == id)
                    {
                        anyDiscountWithCategory = true;
                        break;
                    }
                }
            }

            return !anyDiscountWithCategory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class DiscountService : Service<Discount>, IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
            : base(discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public Task<IEnumerable<Discount>> GetByTagAsync(Guid tagId)
        {
            return _discountRepository.GetByTagAsync(tagId);
        }

        public Task<IEnumerable<Discount>> GetByCategoryAsync(Guid categoryId)
        {
            return _discountRepository.GetByCategoryAsync(categoryId);
        }

        public Task<IEnumerable<Discount>> GetByLocationAsync(Address location)
        {
            return _discountRepository.GetByLocationAsync(location);
        }

        public Task<IEnumerable<Discount>> GetByVendorAsync(Guid vendorId)
        {
            return _discountRepository.GetByVendorAsync(vendorId);
        }
    }
}
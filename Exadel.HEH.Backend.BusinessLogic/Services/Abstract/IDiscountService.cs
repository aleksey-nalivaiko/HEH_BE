using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountService : IService<Discount>
    {
        Task<IEnumerable<Discount>> GetByTagAsync(Guid tagId);

        Task<IEnumerable<Discount>> GetByCategoryAsync(Guid categoryId);

        Task<IEnumerable<Discount>> GetByLocationAsync(Address address);

        Task<IEnumerable<Discount>> GetByVendorAsync(Guid vendorId);
    }
}
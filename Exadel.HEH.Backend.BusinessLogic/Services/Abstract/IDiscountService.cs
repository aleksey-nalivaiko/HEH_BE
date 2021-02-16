using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountService
    {
        Task<IQueryable<DiscountDto>> GetAsync(string searchText = default);

        Task<DiscountExtendedDto> GetByIdAsync(Guid id);

        Task CreateManyAsync(IEnumerable<DiscountDto> discounts);

        Task UpdateManyAsync(IEnumerable<DiscountDto> discounts);

        Task RemoveAsync(Expression<Func<Discount, bool>> expression);
    }
}
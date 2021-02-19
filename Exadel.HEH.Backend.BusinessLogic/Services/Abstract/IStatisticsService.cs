using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IStatisticsService
    {
        Task<IQueryable<DiscountStatisticsDto>> GetStatisticsAsync(
            string searchText = default, DateTime startDate = default, DateTime endDate = default);

        Task IncrementViewsAmountAsync(Guid discountId);

        Task RemoveAsync(Guid discountId);
    }
}
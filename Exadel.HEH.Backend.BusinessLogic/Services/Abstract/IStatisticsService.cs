using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Microsoft.AspNet.OData.Query;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IStatisticsService
    {
        Task<IQueryable<DiscountStatisticsDto>> GetStatisticsAsync(ODataQueryOptions<DiscountStatisticsDto> options,
            string searchText = default, DateTime startDate = default, DateTime endDate = default);

        Task IncrementViewsAmountAsync(Guid discountId);

        Task RemoveAsync(Guid discountId);
    }
}
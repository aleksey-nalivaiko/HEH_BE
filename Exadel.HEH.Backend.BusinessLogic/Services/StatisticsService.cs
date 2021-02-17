using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDiscountRepository _discountRepository;

        public StatisticsService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public Task IncrementViewsAmountAsync(Guid vendorId)
        {
            return _discountRepository.UpdateIncrementAsync(vendorId, v => v.ViewsAmount, 1);
        }
    }
}
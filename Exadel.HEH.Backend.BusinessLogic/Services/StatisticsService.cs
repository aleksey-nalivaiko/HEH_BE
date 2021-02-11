using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IVendorRepository _vendorRepository;

        public StatisticsService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public Task IncrementViewsAmountAsync(Guid vendorId)
        {
            return _vendorRepository.UpdateIncrementAsync(vendorId, v => v.ViewsAmount, 1);
        }
    }
}
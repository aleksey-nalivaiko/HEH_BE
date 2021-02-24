using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class DiscountValidationService : IDiscountValidationService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILocationRepository _locationRepository;

        public DiscountValidationService(IDiscountRepository discountRepository, ILocationRepository locationRepository)
        {
            _discountRepository = discountRepository;
            _locationRepository = locationRepository;
        }

        public async Task<bool> DiscountExists(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return result != null;
        }

        public async Task<bool> DiscountNotExists(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return result is null;
        }

        public async Task<bool> AddressesExist(Guid countryId, Guid cityId, CancellationToken token)
        {
            var country = await _locationRepository.GetByIdAsync(countryId);

            if (country is null)
            {
                return false;
            }

            var city = country.Cities.Where(x => x.Id == cityId).ToList();

            return city.Count != 0;
        }
    }
}
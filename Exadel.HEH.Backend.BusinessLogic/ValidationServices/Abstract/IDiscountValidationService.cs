using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IDiscountValidationService
    {
        Task<bool> DiscountExists(Guid discountId, CancellationToken token = default);

        Task<bool> DiscountNotExists(Guid discountId, CancellationToken token = default);

        Task<bool> AddressesAreUniqueAsync(Guid vendorId, IEnumerable<AddressDto> addresses,
            CancellationToken token);

        Task<bool> AddressesAreFromVendorAsync(Guid discountId, IEnumerable<VendorDto> vendor, CancellationToken token);

        Task<bool> AddressesExist(Guid countryId, Guid cityId, CancellationToken token);
    }
}
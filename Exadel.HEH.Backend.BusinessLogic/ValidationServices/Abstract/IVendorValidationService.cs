using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IVendorValidationService
    {
        Task<bool> VendorExistsAsync(Guid vendorId, CancellationToken token = default);

        Task<bool> VendorNotExistsAsync(Guid vendorId, CancellationToken token = default);

        Task<bool> AddressesCanBeRemovedAsync(Guid vendorId, IEnumerable<AddressDto> addresses, CancellationToken token = default);

        Task<bool> AddressesAreUniqueAsync(Guid vendorId, IEnumerable<AddressDto> addresses, CancellationToken token = default);

        Task<bool> AddressesAreFromVendorAsync(Guid vendorId, IEnumerable<DiscountDto> discounts, CancellationToken token = default);

        Task<bool> PhonesAreUniqueAsync(Guid vendorId, IEnumerable<PhoneDto> phones, CancellationToken token = default);

        Task<bool> PhonesAreFromVendorAsync(Guid vendorId, IEnumerable<DiscountDto> discounts, CancellationToken token = default);
    }
}
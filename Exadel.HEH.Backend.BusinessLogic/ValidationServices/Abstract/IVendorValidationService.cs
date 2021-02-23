using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IVendorValidationService
    {
        Task<bool> VendorExistsAsync(Guid vendorId, CancellationToken token = default);

        Task<bool> VendorNotExistsAsync(Guid vendorId, CancellationToken token = default);

        Task<bool> AddressesCanBeRemovedAsync(Guid vendorId,
            IEnumerable<AddressDto> addresses,
            CancellationToken token = default);

        bool AddressesAreUnique(IEnumerable<int> addressesIds);

        bool AddressesAreFromVendor(VendorDto vendor, IEnumerable<DiscountShortDto> discounts);

        bool PhonesAreUnique(IEnumerable<int> phonesIds);

        Task<bool> PhonesAreFromVendorAsync(VendorDto vendor,
            IEnumerable<DiscountShortDto> discounts,
            CancellationToken cancellationToken = default);

        Task<bool> VendorNameExists(string vendorName, CancellationToken token);

        Task<bool> PhonesAreValid(string phones, CancellationToken token);
    }
}
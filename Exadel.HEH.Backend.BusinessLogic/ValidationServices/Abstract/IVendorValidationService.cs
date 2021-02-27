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

        Task<bool> AddressesCanBeRemovedAsync(VendorDto vendor,
            CancellationToken token = default);

        bool AddressesIdsAreUnique(IEnumerable<int> addressesIds);

        bool AddressesAreUnique(IEnumerable<AddressDto> addresses);

        bool AddressesAreFromVendor(VendorDto vendor, IEnumerable<DiscountShortDto> discounts);

        bool StreetWithCity(AddressDto address);

        bool PhonesAreUnique(IEnumerable<int> phonesIds);

        Task<bool> PhonesAreFromVendorAsync(VendorDto vendor,
            IEnumerable<DiscountShortDto> discounts,
            CancellationToken cancellationToken = default);

        Task<bool> VendorNameExists(string vendorName, CancellationToken token);

        Task<bool> VendorNameChangedAndNotExists(Guid vendorId, string vendorName, CancellationToken token);

        Task<bool> AddressExists(Guid countryId, Guid? cityId, CancellationToken token);

        Task<bool> VendorFromLocationAsync(Guid vendorId, CancellationToken token);
    }
}
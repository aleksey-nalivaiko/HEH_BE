using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class VendorValidationService : IVendorValidationService
    {
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private Vendor _vendor;

        public VendorValidationService(IRepository<Vendor> vendorRepository, IDiscountRepository discountRepository)
        {
            _vendorRepository = vendorRepository;
            _discountRepository = discountRepository;
        }

        public async Task<bool> VendorExistsAsync(Guid vendorId, CancellationToken token)
        {
            await GetVendor(vendorId);

            return _vendor != null;
        }

        public async Task<bool> VendorNotExistsAsync(Guid vendorId, CancellationToken token)
        {
            await GetVendor(vendorId);

            return _vendor is null;
        }

        public async Task<bool> AddressesCanBeRemovedAsync(Guid vendorId, IEnumerable<AddressDto> addresses, CancellationToken token)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
            var vendorAddressesIds = _vendor.Addresses.Select(a => a.Id);
            var newAddressesIds = addresses.Select(a => a.Id).ToList();

            var addressesToBeRemoved = vendorAddressesIds
                .Where(a => !newAddressesIds.Contains(a)).ToList();

            if (addressesToBeRemoved.Any())
            {
                var discountAddresses = _discountRepository.Get().Where(d => d.VendorId == vendorId)
                    .SelectMany(d => d.AddressesIds).Distinct().ToList();

                if (discountAddresses.Any(a => addressesToBeRemoved.Contains(a)))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> AddressesAreUniqueAsync(Guid vendorId, IEnumerable<AddressDto> addresses,
            CancellationToken token)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
            var addressesIds = addresses.Select(a => a.Id).Where(i => i != Guid.Empty).ToList();
            return addressesIds.Count == addressesIds.Distinct().Count();
        }

        public async Task<bool> AddressesAreFromVendorAsync(Guid vendorId, IEnumerable<DiscountDto> discounts, CancellationToken token)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);

            var discountAddressesIds = discounts.SelectMany(d => d.AddressesIds).Distinct().ToList();

            var vendorAddressesIds = _vendor.Addresses.Select(p => p.Id);

            return discountAddressesIds.All(p => vendorAddressesIds.Contains(p));
        }

        public async Task<bool> PhonesAreUniqueAsync(Guid vendorId, IEnumerable<PhoneDto> phones,
            CancellationToken token)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
            var phonesIds = phones.Select(a => a.Id).Where(i => i != Guid.Empty).ToList();
            return phonesIds.Count == phonesIds.Distinct().Count();
        }

        public async Task<bool> PhonesAreFromVendorAsync(Guid vendorId, IEnumerable<DiscountDto> discounts,
            CancellationToken token)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);

            var discountPhonesIds = discounts.SelectMany(d => d.PhonesIds).Distinct().ToList();

            var vendorPhonesIds = _vendor.Phones.Select(p => p.Id);

            return discountPhonesIds.All(p => vendorPhonesIds.Contains(p));
        }

        private async Task GetVendor(Guid vendorId)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
        }
    }
}
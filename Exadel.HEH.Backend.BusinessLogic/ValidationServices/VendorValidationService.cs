﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class VendorValidationService : IVendorValidationService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private Vendor _vendor;

        public VendorValidationService(IVendorRepository vendorRepository, IDiscountRepository discountRepository)
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
            await GetVendor(vendorId);

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

        public bool AddressesAreUnique(IEnumerable<int> addressesIds)
        {
            var addressesIdsList = addressesIds.ToList();
            return addressesIdsList.Count == addressesIdsList.Distinct().Count();
        }

        public bool AddressesAreFromVendor(VendorDto vendor, IEnumerable<DiscountDto> discounts)
        {
            var discountAddressesIds = discounts.SelectMany(d =>
            {
                if (d.AddressesIds != null)
                {
                    return d.AddressesIds;
                }

                return new List<int>();
            }).Distinct().ToList();

            if (vendor.Addresses != null)
            {
                var vendorAddressesIds = vendor.Addresses.Select(p => p.Id);
                return discountAddressesIds.All(p => vendorAddressesIds.Contains(p));
            }

            return true;
        }

        public bool PhonesAreUnique(IEnumerable<int> phonesIds)
        {
            var phonesIdsList = phonesIds.ToList();
            return phonesIdsList.Count == phonesIdsList.Distinct().Count();
        }

        public bool PhonesAreFromVendor(VendorDto vendor, IEnumerable<DiscountDto> discounts)
        {
            var discountPhonesIds = discounts.SelectMany(d =>
            {
                if (d.PhonesIds != null)
                {
                    return d.PhonesIds;
                }

                return new List<int>();
            }).Distinct().ToList();

            if (vendor.Phones != null)
            {
                var vendorPhonesIds = vendor.Phones.Select(p => p.Id);
                return discountPhonesIds.All(p => vendorPhonesIds.Contains(p));
            }

            return true;
        }

        private async Task GetVendor(Guid vendorId)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class VendorValidationService : IVendorValidationService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserService _userService;
        private Vendor _vendor;

        public VendorValidationService(IVendorRepository vendorRepository, IDiscountRepository discountRepository,
            ILocationRepository locationRepository, IUserService userService)
        {
            _vendorRepository = vendorRepository;
            _discountRepository = discountRepository;
            _locationRepository = locationRepository;
            _userService = userService;
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
                    .SelectMany(d => d.Addresses).Distinct().ToList();

                if (discountAddresses.Any(a => addressesToBeRemoved.Contains(a.Id)))
                {
                    return false;
                }
            }

            return true;
        }

        public bool AddressesIdsAreUnique(IEnumerable<int> addressesIds)
        {
            var addressesIdsList = addressesIds.ToList();
            return addressesIdsList.Count == addressesIdsList.Distinct().Count();
        }

        public bool AddressesAreUnique(IEnumerable<AddressDto> addresses)
        {
            var addressesList = addresses.ToList();

            return addressesList.Count == addressesList
                .GroupBy(a => new { a.CountryId, a.CityId, a.Street })
                .Select(g => g)
                .Count();
        }

        public bool AddressesAreFromVendor(VendorDto vendor, IEnumerable<DiscountShortDto> discounts)
        {
            var discountAddressesIds = discounts.SelectMany(d => d.AddressesIds)
                .Distinct()
                .ToList();

            var vendorAddressesIds = vendor.Addresses.Select(p => p.Id);

            return discountAddressesIds.All(id => vendorAddressesIds.Contains(id));
        }

        public bool PhonesAreUnique(IEnumerable<int> phonesIds)
        {
            var phonesIdsList = phonesIds.ToList();
            return phonesIdsList.Count == phonesIdsList.Distinct().Count();
        }

        public async Task<bool> PhonesAreFromVendorAsync(VendorDto vendor,
            IEnumerable<DiscountShortDto> discounts, CancellationToken cancellationToken)
        {
            var discountPhonesIds = discounts.SelectMany(d =>
                {
                    if (d.PhonesIds != null)
                    {
                        return d.PhonesIds;
                    }

                    return new List<int>();
                })
                .Distinct()
                .ToList();

            if (vendor.Phones != null)
            {
                var newPhonesIds = vendor.Phones.Select(a => a.Id).ToList();

                if (vendor.Id != Guid.Empty)
                {
                    await GetVendor(vendor.Id);
                    if (_vendor != null)
                    {
                        var vendorPhonesIds = _vendor.Phones.Select(a => a.Id);

                        var phonesToBeRemoved = vendorPhonesIds
                            .Where(p => !newPhonesIds.Contains(p)).ToList();

                        if (phonesToBeRemoved.Any())
                        {
                            return discountPhonesIds.All(p => newPhonesIds.Contains(p)
                                                              || phonesToBeRemoved.Contains(p));
                        }
                    }
                }
                else
                {
                    return discountPhonesIds.All(p => newPhonesIds.Contains(p));
                }
            }
            else
            {
                if (discountPhonesIds.Any() && vendor.Id == Guid.Empty)
                {
                    return false;
                }
            }

            return true;
        }

        public Task<bool> VendorNameExists(string vendorName, CancellationToken token)
        {
            var vendor = _vendorRepository.Get().FirstOrDefault(x => x.Name == vendorName);

            return Task.FromResult(vendor is null);
        }

        public async Task<bool> VendorNameChangedAndNotExists(Guid vendorId, string vendorName, CancellationToken token)
        {
            await GetVendor(vendorId);

            if (_vendor.Name == vendorName)
            {
                return true;
            }

            return !_vendorRepository.Get().Any(x => x.Name == vendorName);
        }

        public async Task<bool> AddressExists(Guid countryId, Guid? cityId, CancellationToken token)
        {
            var country = await _locationRepository.GetByIdAsync(countryId);

            if (country is null)
            {
                return false;
            }

            if (cityId is null)
            {
                return true;
            }

            var cities = country.Cities.Where(x => x.Id == cityId).ToList();

            return cities.Count != 0;
        }

        public async Task<bool> VendorFromLocationAsync(Guid vendorId, CancellationToken token)
        {
            var user = await _userService.GetProfileAsync();

            var vendor = await _vendorRepository.GetByIdAsync(vendorId);

            var countryCities = vendor.Addresses
                .GroupBy(a => a.CountryId)
                .Select(g =>
                    new KeyValuePair<Guid, IEnumerable<Guid?>>(
                        g.Key, g.Select(a => a.CityId).Where(i => i.HasValue)))
                .ToDictionary(a => a.Key, a => a.Value);

            return countryCities.ContainsKey(user.Address.CountryId) && (!countryCities[user.Address.CountryId].Any()
                                                                         || countryCities[user.Address.CountryId]
                                                                             .Contains(user.Address.CityId));
        }

        private async Task GetVendor(Guid vendorId)
        {
            _vendor ??= await _vendorRepository.GetByIdAsync(vendorId);
        }
    }
}
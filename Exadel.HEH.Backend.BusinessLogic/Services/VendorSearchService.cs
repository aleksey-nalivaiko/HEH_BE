using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public abstract class VendorSearchService
    {
        protected readonly ISearchRepository<VendorSearch> SearchRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        protected VendorSearchService(ISearchRepository<VendorSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService,
            IMapper mapper)
        {
            SearchRepository = searchRepository;
            _vendorRepository = vendorRepository;
            _discountRepository = discountRepository;
            _locationService = locationService;
            _categoryService = categoryService;
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task CreateAsync(VendorDto vendor)
        {
            var search = await GetSearchAsync(vendor);
            await SearchRepository.CreateAsync(search);
        }

        public async Task UpdateAsync(VendorDto vendor)
        {
            var search = await GetSearchAsync(vendor);
            await SearchRepository.UpdateAsync(search);
        }

        public Task RemoveAsync(Guid id)
        {
            return SearchRepository.RemoveAsync(id);
        }

        public async Task ReindexAsync()
        {
            await SearchRepository.RemoveAllAsync();

            var vendors = await _vendorRepository.GetAllAsync();
            var allDiscounts = await _discountRepository.GetAllAsync();

            var vendorsDtos = vendors.GroupJoin(
                allDiscounts,
                vendor => vendor.Id,
                discount => discount.VendorId,
                (vendor, discounts) =>
                {
                    var vendorDto = _mapper.Map<VendorDto>(vendor);
                    vendorDto.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(discounts);
                    return vendorDto;
                });

            var searchList = await GetAllSearch(vendorsDtos);

            await SearchRepository.CreateManyAsync(searchList);
        }

        private async Task<IEnumerable<VendorSearch>> GetAllSearch(IEnumerable<VendorDto> vendors)
        {
            var searchTasks = vendors.Select(GetSearchAsync);

            return await Task.WhenAll(searchTasks);
        }

        private async Task<VendorSearch> GetSearchAsync(VendorDto vendor)
        {
            var countriesIds = vendor.Addresses.Select(a => a.CountryId);
            var citiesIds = vendor.Addresses.Select(a => a.CityId);
            var streets = vendor.Addresses.Select(a => a.Street).ToList();

            var locations = (await _locationService.GetByIdsAsync(countriesIds)).ToList();
            var countries = locations.Select(location => location.Country).ToList();
            var cities = locations.SelectMany(location => location.Cities
                .Where(c => citiesIds.Contains(c.Id))
                .Select(c => c.Name)).ToList();

            var discounts = vendor.Discounts.ToList();

            var categoriesNames = (await _categoryService.GetByIdsAsync(discounts.Select(c => c.CategoryId)))
                .Select(c => c.Name)
                .ToList();

            var tagsNames = (await _tagService.GetByIdsAsync(discounts.SelectMany(d => d.TagsIds)))
                .Select(t => t.Name)
                .ToList();

            var search = new VendorSearch
            {
                Id = vendor.Id,
                Discounts = discounts.Select(d => d.Conditions).ToList(),
                Vendor = vendor.Name,
                Categories = categoriesNames,
                Tags = tagsNames,
                Countries = countries,
                Cities = cities,
                Streets = streets
            };
            return search;
        }
    }
}
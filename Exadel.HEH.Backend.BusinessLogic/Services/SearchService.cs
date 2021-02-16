using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public abstract class SearchService
    {
        protected readonly ISearchRepository SearchRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        protected SearchService(ISearchRepository searchRepository,
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

        public async Task CreateAsync(DiscountDto discount)
        {
            var search = await GetSearchAsync(discount);
            await SearchRepository.CreateAsync(search);
        }

        public async Task UpdateAsync(DiscountDto discount)
        {
            var search = await GetSearchAsync(discount);
            await SearchRepository.UpdateAsync(search);
        }

        public Task RemoveAsync(Guid id)
        {
            return SearchRepository.RemoveAsync(id);
        }

        public async Task Reindex()
        {
            await SearchRepository.RemoveAllAsync();

            var discounts = await _discountRepository.GetAllAsync();
            var searchList = await GetAllSearch(discounts);

            await SearchRepository.CreateManyAsync(searchList);
        }

        private async Task<IEnumerable<Search>> GetAllSearch(IEnumerable<Discount> discounts)
        {
            var searchTasks = discounts.Select(d => GetSearchAsync(_mapper.Map<DiscountDto>(d)));

            return await Task.WhenAll(searchTasks);
        }

        private async Task<Search> GetSearchAsync(DiscountDto discount)
        {
            var vendorAddresses = (await _vendorRepository.GetByIdAsync(discount.VendorId)).Addresses;

            var discountAddresses = vendorAddresses
                .Where(a => discount.AddressesIds.Contains(a.Id))
                .ToList();

            var countriesIds = discountAddresses.Select(a => a.CountryId);
            var locations = (await _locationService.GetByIdsAsync(countriesIds)).ToList();
            var countries = locations.Select(location => location.Country).ToList();

            var citiesIds = discountAddresses.Select(a => a.CityId);
            var cities = locations.SelectMany(location =>
                location.Cities
                    .Where(c => citiesIds.Contains(c.Id))
                    .Select(c => c.Name)).ToList();

            var streets = discountAddresses.Select(a => a.Street).ToList();

            var category = await _categoryService.GetByIdAsync(discount.CategoryId);
            var tagsNames = (await _tagService.GetByIds(discount.TagsIds))
                .Select(t => t.Name).ToList();

            var search = new Search
            {
                Id = discount.Id,
                Discount = discount.Conditions,
                Vendor = discount.VendorName,
                Category = category.Name,
                Tags = tagsNames,
                Countries = countries,
                Cities = cities,
                Streets = streets
            };
            return search;
        }
    }
}
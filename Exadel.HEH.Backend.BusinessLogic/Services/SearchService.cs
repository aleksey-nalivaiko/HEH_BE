using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public SearchService(ISearchRepository searchRepository,
            IVendorRepository vendorRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService)
        {
            _searchRepository = searchRepository;
            _vendorRepository = vendorRepository;
            _locationService = locationService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task CreateAsync(DiscountDto discount)
        {
            var search = await GetSearch(discount);
            await _searchRepository.CreateAsync(search);
        }

        public async Task UpdateAsync(DiscountDto discount)
        {
            var search = await GetSearch(discount);
            await _searchRepository.UpdateAsync(search);
        }

        public Task RemoveAsync(Guid id)
        {
            return _searchRepository.RemoveAsync(id);
        }

        private async Task<Search> GetSearch(DiscountDto discount)
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
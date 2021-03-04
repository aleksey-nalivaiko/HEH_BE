using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    [ExcludeFromCodeCoverage]
    public abstract class DiscountSearchService
    {
        protected readonly ISearchRepository<DiscountSearch> SearchRepository;
        protected readonly IDiscountRepository DiscountRepository;

        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        protected DiscountSearchService(ISearchRepository<DiscountSearch> searchRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService)
        {
            SearchRepository = searchRepository;
            DiscountRepository = discountRepository;
            _locationService = locationService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task CreateAsync(Discount discount)
        {
            var search = await GetSearchAsync(discount);
            await SearchRepository.CreateAsync(search);
        }

        public async Task UpdateAsync(Discount discount)
        {
            var search = await GetSearchAsync(discount);
            await SearchRepository.UpdateAsync(search);
        }

        public Task RemoveAsync(Guid id)
        {
            return SearchRepository.RemoveAsync(id);
        }

        public async Task ReindexAsync()
        {
            await SearchRepository.RemoveAllAsync();

            var discounts = await DiscountRepository.GetAllAsync();
            var searchList = await GetAllSearchAsync(discounts);

            await SearchRepository.CreateManyAsync(searchList);
        }

        private async Task<IEnumerable<DiscountSearch>> GetAllSearchAsync(IEnumerable<Discount> discounts)
        {
            var searchTasks = discounts.Select(GetSearchAsync);

            return await Task.WhenAll(searchTasks);
        }

        private async Task<DiscountSearch> GetSearchAsync(Discount discount)
        {
            var countries = new List<string>();
            var cities = new List<string>();
            var streets = new List<string>();

            if (discount.Addresses != null && discount.Addresses.Any())
            {
                var discountAddresses = discount.Addresses;

                var countriesIds = discountAddresses.Select(a => a.CountryId).Distinct();
                var locations = (await _locationService.GetByIdsAsync(countriesIds)).ToList();
                countries = locations.Select(location => location.Country).ToList();

                var citiesIds = discountAddresses.Select(a => a.CityId).Distinct();
                cities = locations.SelectMany(location =>
                    location.Cities
                        .Where(c => citiesIds.Contains(c.Id))
                        .Select(c => c.Name)).ToList();

                streets = discountAddresses.Select(a => a.Street).ToList();
            }

            var category = await _categoryService.GetByIdAsync(discount.CategoryId);

            var tagsNames = new List<string>();

            if (discount.TagsIds != null && discount.TagsIds.Any())
            {
                tagsNames = (await _tagService.GetByIdsAsync(discount.TagsIds))
                    .Select(t => t.Name).ToList();
            }

            var search = new DiscountSearch
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
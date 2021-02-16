using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneDiscountSearchService : DiscountSearchService,
        ISearchService<Discount, DiscountDto>
    {
        public LuceneDiscountSearchService(
            ISearchRepository<DiscountSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService, IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository, locationService,
                categoryService, tagService, mapper)
        {
        }

        public IQueryable<Discount> Search(IQueryable<Discount> allDiscounts, string searchText)
        {
            var searchResults = GetSearchResultsAsync(searchText).Result;
            var searchResultsIds = searchResults.Select(s => s.Id).ToList();

            return allDiscounts.Where(d => searchResultsIds.Contains(d.Id)).AsEnumerable()
                .OrderBy(d => searchResultsIds.IndexOf(d.Id)).AsQueryable();
        }

        private async Task<IEnumerable<DiscountSearch>> GetSearchResultsAsync(string searchText)
        {
            var path = JsonSerializer.Serialize(new object[]
            {
                nameof(DiscountSearch.Discount).ToLower(),
                nameof(DiscountSearch.Vendor).ToLower(),
                nameof(DiscountSearch.Category).ToLower(),
                nameof(DiscountSearch.Tags).ToLower(),
                nameof(DiscountSearch.Countries).ToLower(),
                nameof(DiscountSearch.Cities).ToLower(),
                nameof(DiscountSearch.Streets).ToLower()
            });

            return await SearchRepository.SearchAsync(path, searchText);
        }
    }
}
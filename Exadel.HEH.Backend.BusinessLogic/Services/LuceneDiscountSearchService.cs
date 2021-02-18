using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneDiscountSearchService : DiscountSearchService,
        ISearchService<Discount, Discount>
    {
        private readonly string _searchPath;

        public LuceneDiscountSearchService(
            ISearchRepository<DiscountSearch> searchRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService)
            : base(searchRepository, discountRepository, locationService,
                categoryService, tagService)
        {
            _searchPath = JsonSerializer.Serialize(new object[]
            {
                nameof(DiscountSearch.Discount).ToLower(),
                nameof(DiscountSearch.Vendor).ToLower(),
                nameof(DiscountSearch.Category).ToLower(),
                nameof(DiscountSearch.Tags).ToLower(),
                nameof(DiscountSearch.Countries).ToLower(),
                nameof(DiscountSearch.Cities).ToLower(),
                nameof(DiscountSearch.Streets).ToLower()
            });
        }

        public async Task<IEnumerable<Discount>> SearchAsync(string searchText)
        {
            var allDiscounts = DiscountRepository.Get();

            if (searchText != null)
            {
                var searchResults = await GetSearchResultsAsync(searchText);
                var searchResultsIds = searchResults.Select(s => s.Id).ToList();

                return allDiscounts
                    .Where(d => searchResultsIds.Contains(d.Id))
                    .AsEnumerable()
                    .OrderBy(d => searchResultsIds.IndexOf(d.Id));
            }

            return allDiscounts;
        }

        private async Task<IEnumerable<DiscountSearch>> GetSearchResultsAsync(string searchText)
        {
            return await SearchRepository.SearchAsync(_searchPath, searchText);
        }
    }
}
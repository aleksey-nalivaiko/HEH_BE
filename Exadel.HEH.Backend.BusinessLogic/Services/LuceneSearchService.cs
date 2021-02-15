using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneSearchService : SearchService, ISearchService
    {
        public LuceneSearchService(ISearchRepository searchRepository,
            IVendorRepository vendorRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService)
            : base(searchRepository, vendorRepository, locationService, categoryService, tagService)
        {
        }

        public IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText)
        {
            var searchResults = GetSearchResults(searchText).Result;
            var searchResultsIds = searchResults.Select(s => s.Id).ToList();

            return allDiscounts.Where(d => searchResultsIds.Contains(d.Id)).AsEnumerable()
                .OrderBy(d => searchResultsIds.IndexOf(d.Id)).AsQueryable();
        }

        private async Task<IEnumerable<Search>> GetSearchResults(string searchText)
        {
            var path = JsonSerializer.Serialize(new object[]
            {
                nameof(Search.Discount).ToLower(),
                nameof(Search.Vendor).ToLower(),
                nameof(Search.Category).ToLower(),
                nameof(Search.Tags).ToLower(),
                nameof(Search.Countries).ToLower(),
                nameof(Search.Cities).ToLower(),
                nameof(Search.Streets).ToLower()
            });

            return await SearchRepository.SearchAsync(path, searchText);
        }
    }
}
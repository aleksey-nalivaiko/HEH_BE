using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneDiscountSearchService : IDiscountSearchService
    {
        private readonly ISearchRepository _searchRepository;

        public LuceneDiscountSearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText)
        {
            var searchResults = GetSearchResults(searchText).Result;

            return allDiscounts.Where(d => searchResults.Select(s => s.Id).Contains(d.Id));
        }

        private async Task<IEnumerable<Search>> GetSearchResults(string searchText)
        {
            var path = JsonSerializer.Serialize(new object[]
            {
                "discount",
                "vendor",
                "category",
                new
                {
                    Wildcard = new[] { "tags.*", "countries.*", "cities.*", "streets.*" }
                }
            });

            return await _searchRepository.SearchAsync(path, searchText);
        }
    }
}
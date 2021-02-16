using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneVendorSearchService : VendorSearchService, ISearchService<Vendor, VendorDto>
    {
        public LuceneVendorSearchService(
            ISearchRepository<VendorSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService, IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository, locationService,
                categoryService, tagService, mapper)
        {
        }

        public IQueryable<Vendor> Search(IQueryable<Vendor> allVendors, string searchText)
        {
            var searchResults = GetSearchResults(searchText).Result;
            var searchResultsIds = searchResults.Select(s => s.Id).ToList();

            return allVendors.Where(d => searchResultsIds.Contains(d.Id)).AsEnumerable()
                .OrderBy(d => searchResultsIds.IndexOf(d.Id)).AsQueryable();
        }

        private async Task<IEnumerable<VendorSearch>> GetSearchResults(string searchText)
        {
            var path = JsonSerializer.Serialize(new object[]
            {
                nameof(VendorSearch.Vendor).ToLower(),
                nameof(VendorSearch.Discounts).ToLower(),
                nameof(VendorSearch.Categories).ToLower(),
                nameof(VendorSearch.Tags).ToLower(),
                nameof(VendorSearch.Countries).ToLower(),
                nameof(VendorSearch.Cities).ToLower(),
                nameof(VendorSearch.Streets).ToLower()
            });

            return await SearchRepository.SearchAsync(path, searchText);
        }
    }
}
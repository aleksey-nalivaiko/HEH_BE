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
    public class LuceneVendorSearchService : VendorSearchService,
        ISearchService<VendorSearch, VendorDto>
    {
        private readonly string _searchPath;

        public LuceneVendorSearchService(
            ISearchRepository<VendorSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService,
            IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository, locationService,
                categoryService, tagService, mapper)
        {
            _searchPath = JsonSerializer.Serialize(new object[]
            {
                nameof(VendorSearch.Vendor).ToLower(),
                nameof(VendorSearch.Discounts).ToLower(),
                nameof(VendorSearch.Categories).ToLower(),
                nameof(VendorSearch.Tags).ToLower(),
                nameof(VendorSearch.Countries).ToLower(),
                nameof(VendorSearch.Cities).ToLower(),
                nameof(VendorSearch.Streets).ToLower()
            });
        }

        public IQueryable<VendorSearch> Search(string searchText)
        {
            return searchText != null ?
                GetSearchResultsAsync(searchText).Result.AsQueryable() : SearchRepository.Get();
        }

        private async Task<IEnumerable<VendorSearch>> GetSearchResultsAsync(string searchText)
        {
            return await SearchRepository.SearchAsync(_searchPath, searchText);
        }
    }
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    [ExcludeFromCodeCoverage]
    public class LocalVendorSearchService : VendorSearchService, IVendorSearchService
    {
        public LocalVendorSearchService(ISearchRepository<VendorSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService,
            IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository,
                locationService, categoryService, tagService, mapper)
        {
        }

        public Task<IEnumerable<VendorSearch>> SearchAsync(string searchText)
        {
            if (searchText != null)
            {
                var allVendors = SearchRepository.Get();

                return Task.FromResult(allVendors.Where(v =>
                    v.Vendor.ToLower().Contains(searchText.ToLower()))
                    .AsEnumerable());
            }

            return SearchRepository.GetAllAsync();
        }
    }
}
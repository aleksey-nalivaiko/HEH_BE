using System.Linq;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalVendorSearchService : VendorSearchService, ISearchService<Vendor, VendorDto>
    {
        public LocalVendorSearchService(ISearchRepository<VendorSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService, IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository, locationService, categoryService, tagService, mapper)
        {
        }

        public IQueryable<Vendor> Search(IQueryable<Vendor> allVendors, string searchText)
        {
            var lowerSearchText = searchText.ToLower();
            return allVendors.Where(v => v.Name.ToLower().Contains(lowerSearchText));
        }
    }
}
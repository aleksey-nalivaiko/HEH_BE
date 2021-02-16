using System.Linq;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalDiscountSearchService : DiscountSearchService,
        ISearchService<Discount, DiscountDto>
    {
        public LocalDiscountSearchService(ISearchRepository<DiscountSearch> searchRepository,
            IVendorRepository vendorRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService, IMapper mapper)
            : base(searchRepository, vendorRepository, discountRepository, locationService, categoryService, tagService, mapper)
        {
        }

        public IQueryable<Discount> Search(IQueryable<Discount> allDiscounts, string searchText)
        {
            var lowerSearchText = searchText.ToLower();
            return allDiscounts.Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                                           || d.VendorName.ToLower().Contains(lowerSearchText));
        }
    }
}
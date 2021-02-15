using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalSearchService : SearchService, ISearchService
    {
        public LocalSearchService(ISearchRepository searchRepository,
            IVendorRepository vendorRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService)
            : base(searchRepository, vendorRepository, locationService, categoryService, tagService)
        {
        }

        public IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText)
        {
            var lowerSearchText = searchText.ToLower();
            return allDiscounts.Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                                           || d.VendorName.ToLower().Contains(lowerSearchText));
        }
    }
}
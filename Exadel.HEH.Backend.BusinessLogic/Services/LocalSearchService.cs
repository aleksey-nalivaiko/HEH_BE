using System.Linq;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalSearchService : SearchService, ISearchService
    {
        public LocalSearchService(ISearchRepository searchRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService, ITagService tagService)
            : base(searchRepository, discountRepository, locationService, categoryService, tagService)
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
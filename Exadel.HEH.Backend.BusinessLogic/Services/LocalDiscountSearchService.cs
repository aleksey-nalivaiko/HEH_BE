using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalDiscountSearchService : DiscountSearchService,
        ISearchService<Discount, Discount>
    {
        public LocalDiscountSearchService(ISearchRepository<DiscountSearch> searchRepository,
            IDiscountRepository discountRepository,
            ILocationService locationService,
            ICategoryService categoryService,
            ITagService tagService)
            : base(searchRepository, discountRepository, locationService, categoryService, tagService)
        {
        }

        public IQueryable<Discount> Search(string searchText)
        {
            var allDiscounts = DiscountRepository.Get();

            if (searchText != null)
            {
                var lowerSearchText = searchText.ToLower();

                return allDiscounts.Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                                               || d.VendorName.ToLower().Contains(lowerSearchText));
            }

            return allDiscounts;
        }
    }
}
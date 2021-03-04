using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    [ExcludeFromCodeCoverage]
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

        public Task<IEnumerable<Discount>> SearchAsync(string searchText)
        {
            if (searchText != null)
            {
                var allDiscounts = DiscountRepository.Get();
                var lowerSearchText = searchText.ToLower();

                return Task.FromResult(allDiscounts
                    .Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                                || d.VendorName.ToLower().Contains(lowerSearchText))
                    .AsEnumerable());
            }

            return DiscountRepository.GetAllAsync();
        }
    }
}
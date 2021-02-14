using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocalDiscountSearchService : IDiscountSearchService
    {
        public IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText)
        {
            var lowerSearchText = searchText.ToLower();
            return allDiscounts.Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                                           || d.VendorName.ToLower().Contains(lowerSearchText));
        }
    }
}
using System.Linq;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IDiscountSearchService
    {
        IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText);
    }
}
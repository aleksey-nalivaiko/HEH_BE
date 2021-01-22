using System.Linq;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IQueryable<Discount> GetAll();
    }
}
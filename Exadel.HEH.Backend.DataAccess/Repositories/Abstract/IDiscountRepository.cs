using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<IEnumerable<Discount>> GetByTagAsync(Guid tagId);

        Task<IEnumerable<Discount>> GetByCategoryAsync(Guid categoryId);

        Task<IEnumerable<Discount>> GetByLocationAsync(Address location);

        Task<IEnumerable<Discount>> GetByVendorAsync(Guid vendorId);
    }
}
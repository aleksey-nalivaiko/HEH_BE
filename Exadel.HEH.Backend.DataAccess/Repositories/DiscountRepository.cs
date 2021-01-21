using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class DiscountRepository : MongoRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(IDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Discount>> GetByTagAsync(Guid tagId)
        {
            var result = await Context.GetAll<Discount>()
                .Where(x => x.Tags.Any(a => a == tagId)).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Discount>> GetByCategoryAsync(Guid categoryId)
        {
            var result = await Context.GetAll<Discount>()
                .Where(x => x.CategoryId == categoryId).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Discount>> GetByLocationAsync(Address address)
        {
            var result = await Context.GetAll<Discount>()
                .Where(x => x.Addresses.Any(a => a.Country == address.Country && a.City == address.City)).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Discount>> GetByVendorAsync(Guid vendorId)
        {
            var result = await Context.GetAll<Discount>()
                .Where(x => x.VendorId == vendorId).ToListAsync();

            return result;
        }
    }
}
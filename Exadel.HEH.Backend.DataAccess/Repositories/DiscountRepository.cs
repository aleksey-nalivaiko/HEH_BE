using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class DiscountRepository : MongoRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(IMongoDatabase database)
            : base(database)
        {
        }

        public async Task<IEnumerable<Discount>> GetByTagAsync(Guid tagId)
        {
            var filter = Builders<Discount>.Filter
                .AnyEq(discount => discount.Tags, tagId);

            return await GetCollection()
                .Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Discount>> GetByCategoryAsync(Guid categoryId)
        {
            var filter = Builders<Discount>.Filter
                .Eq(discount => discount.CategoryId, categoryId);

            return await GetCollection()
                .Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Discount>> GetByLocationAsync(Address location)
        {
            var filter = Builders<Discount>.Filter
                .Where(discount => discount.Addresses.Any(address =>
                    address.Country == location.Country && address.City == location.City));

            return await GetCollection()
                .Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Discount>> GetByVendorAsync(Guid vendorId)
        {
            var filter = Builders<Discount>.Filter
                .Eq(discount => discount.VendorId, vendorId);

            return await GetCollection()
                .Find(filter).ToListAsync();
        }
    }
}
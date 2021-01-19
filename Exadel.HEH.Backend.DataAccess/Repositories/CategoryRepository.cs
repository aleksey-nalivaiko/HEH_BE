using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase database)
            : base(database)
        {
        }

        public Task<Category> GetByTagAsync(Guid tagId)
        {
            var categoryCollection = Database.GetCollection<Category>(nameof(Category));
            var tagCollection = Database.GetCollection<Tag>(nameof(Tag));
            var tag = tagCollection
                .Find(Builders<Tag>.Filter.Eq(x => x.Id, tagId))
                .FirstOrDefaultAsync();
            return categoryCollection
                .Find(Builders<Category>.Filter.Eq(x => x.Id, tag.Result.Id))
                .FirstOrDefaultAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class CategoryRepository : MongoRepository<Category>
    {
        public CategoryRepository(string connectionString)
            : base(connectionString)
        {
        }

        public Task CreateAsync(Category categoryItem)
        {
            return CreateBaseAsync(categoryItem);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return GetAllBaseAsync();
        }

        public Task<Category> GetByIdAsync(Guid id)
        {
            return GetByIdBaseAsync(id);
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

        public Task RemoveAsync(Guid id)
        {
            return RemoveBaseAsync(id);
        }

        public Task UpdateAsync(Guid id, Category categoryItem)
        {
            return UpdateBaseAsync(id, categoryItem);
        }
    }
}

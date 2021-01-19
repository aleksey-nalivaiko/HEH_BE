using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>
    {
        public TagRepository(string connectionString)
            : base(connectionString)
        {
        }

        public Task CreateAsync(Tag tagItem)
        {
            return CreateBaseAsync(tagItem);
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            return GetAllBaseAsync();
        }

        public async Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            var tagCollection = Database.GetCollection<Tag>(nameof(Tag));
            return await tagCollection
                .Find(Builders<Tag>.Filter.Eq(x => x.CategoryId, categoryId))
                .ToListAsync();
        }

        public Task<Tag> GetByIdAsync(Guid id)
        {
            return GetByIdBaseAsync(id);
        }

        public Task RemoveAsync(Guid id)
        {
            return RemoveBaseAsync(id);
        }

        public Task UpdateAsync(Guid id, Tag tagItem)
        {
            return UpdateBaseAsync(id, tagItem);
        }
    }
}

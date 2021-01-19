using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        private readonly IMongoDatabase _database;

        public CategoryRepository(string connectionString)
            : base(connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
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

        public async Task<Category> GetByTagAsync(Guid tagId)
        {
            var categoryCollection = _database.GetCollection<Category>(typeof(Category).Name);
            var tagCollection = _database.GetCollection<Models.Tag>(typeof(Models.Tag).Name);
            var tag = tagCollection.Find(Builders<Models.Tag>.Filter.Eq(x => x.Id, tagId)).FirstOrDefaultAsync();
            return await categoryCollection.Find(Builders<Category>.Filter.Eq(x => x.Id, tag.Result.Id)).FirstOrDefaultAsync();
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

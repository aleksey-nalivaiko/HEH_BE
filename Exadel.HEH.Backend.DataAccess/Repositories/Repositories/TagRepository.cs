using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Models.Tag>, ITagRepository
    {
        private readonly IMongoDatabase _database;

        public TagRepository(string connectionString)
            : base(connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
        }

        public Task CreateAsync(Models.Tag tagItem)
        {
            return CreateBaseAsync(tagItem);
        }

        public Task<IEnumerable<Models.Tag>> GetAllAsync()
        {
            return GetAllBaseAsync();
        }

        public async Task<IEnumerable<Models.Tag>> GetByCategotyAsync(Guid categoryId)
        {
            var tagCollection = _database.GetCollection<Models.Tag>(typeof(Models.Tag).Name);
            return await tagCollection.Find(Builders<Models.Tag>.Filter.Eq(x => x.CategoryId, categoryId)).ToListAsync();
        }

        public Task<Models.Tag> GetByIdAsync(Guid id)
        {
            return GetByIdBaseAsync(id);
        }

        public Task RemoveAsync(Guid id)
        {
            return RemoveBaseAsync(id);
        }

        public Task UpdateAsync(Guid id, Models.Tag tagItem)
        {
            return UpdateBaseAsync(id, tagItem);
        }
    }
}

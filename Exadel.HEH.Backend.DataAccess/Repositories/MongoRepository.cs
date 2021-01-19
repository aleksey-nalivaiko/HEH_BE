using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public abstract class MongoRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
        }

        public async Task<IEnumerable<TDocument>> GetAllBaseAsync()
        {
            return await GetCollection()
                .Find(Builders<TDocument>.Filter.Empty)
                .ToListAsync();
        }

        public async Task<TDocument> GetByIdBaseAsync(Guid id)
        {
            return await GetCollection().Find(Builders<TDocument>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }

        public Task RemoveBaseAsync(Guid id)
        {
            return GetCollection().DeleteOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id));
        }

        public Task CreateBaseAsync(TDocument item)
        {
            return GetCollection().InsertOneAsync(item);
        }

        public Task UpdateBaseAsync(Guid id, TDocument item)
        {
            return GetCollection().ReplaceOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id), item);
        }

        private IMongoCollection<TDocument> GetCollection()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }
}
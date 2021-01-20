using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public abstract class MongoRepository<TDocument> : IRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        protected readonly IMongoDatabase Database;

        protected MongoRepository(IMongoDatabase database)
        {
            Database = database;
        }

        public virtual async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return await GetCollection()
                .Find(Builders<TDocument>.Filter.Empty)
                .ToListAsync();
        }

        public virtual Task<TDocument> GetByIdAsync(Guid id)
        {
            return GetCollection()
                .Find(Builders<TDocument>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        public virtual Task RemoveAsync(Guid id)
        {
            return GetCollection()
                .DeleteOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id));
        }

        public virtual Task CreateAsync(TDocument item)
        {
            return GetCollection().InsertOneAsync(item);
        }

        public virtual Task UpdateAsync(Guid id, TDocument item)
        {
            return GetCollection()
                .ReplaceOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, id), item);
        }

        protected IMongoCollection<TDocument> GetCollection()
        {
            return Database.GetCollection<TDocument>(typeof(TDocument).Name);
        }
    }
}
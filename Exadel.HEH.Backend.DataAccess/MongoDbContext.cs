using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess
{
    public class MongoDbContext : IDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            SetupConventions();
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
        }

        public IQueryable<T> GetAll<T>()
            where T : class, IDataModel, new()
        {
            return GetCollection<T>().AsQueryable();
        }

        public virtual Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>()
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        public virtual Task RemoveAsync<T>(Guid id)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>()
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }

        public virtual Task CreateAsync<T>(T item)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>().InsertOneAsync(item);
        }

        public virtual Task UpdateAsync<T>(T item)
            where T : class, IDataModel, new()
{
            return GetCollection<T>()
                .ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, item.Id), item);
        }

        protected IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private void SetupConventions()
        {
            // https://jira.mongodb.org/projects/CSHARP/issues/CSHARP-3179?filter=allopenissues
            // https://jira.mongodb.org/browse/CSHARP-3195
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var elementNamePack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
            };
            ConventionRegistry.Register(nameof(CamelCaseElementNameConvention),
                elementNamePack, t => true);

            var enumPack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register(nameof(EnumRepresentationConvention),
                enumPack, t => true);
        }
    }
}
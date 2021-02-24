using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            where T : class, new()
        {
            return GetCollection<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            return await GetCollection<T>()
                .Find(Builders<T>.Filter.Where(expression)).ToListAsync();
        }

        public Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>()
                .Find(Builders<T>.Filter.Eq(x => x.Id, id))
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOneAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            return GetCollection<T>().Find(Builders<T>.Filter.Where(expression)).FirstOrDefaultAsync();
        }

        public Task RemoveAsync<T>(Guid id)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>()
                .DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Id, id));
        }

        public Task RemoveAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            return GetCollection<T>().DeleteManyAsync(Builders<T>.Filter.Where(expression));
        }

        public Task RemoveAllAsync<T>()
            where T : class, new()
        {
            return GetCollection<T>().DeleteManyAsync(Builders<T>.Filter.Empty);
        }

        public Task CreateAsync<T>(T item)
            where T : class, new()
        {
            return GetCollection<T>().InsertOneAsync(item);
        }

        public Task CreateManyAsync<T>(IEnumerable<T> items)
            where T : class, new()
        {
            return GetCollection<T>().InsertManyAsync(items);
        }

        public Task UpdateAsync<T>(T item)
            where T : class, IDataModel, new()
        {
            return GetCollection<T>()
                .ReplaceOneAsync(Builders<T>.Filter.Eq(x => x.Id, item.Id), item);
        }

        public Task UpdateManyAsync<T>(IEnumerable<T> items)
            where T : class, IDataModel, new()
        {
            var requests = new List<ReplaceOneModel<T>>();
            var itemsList = items.ToList();

            foreach (var item in itemsList)
            {
                if (item.Id == default)
                {
                    item.Id = Guid.NewGuid();
                }

                var filter = Builders<T>.Filter.Eq(x => x.Id, item.Id);

                var request = new ReplaceOneModel<T>(filter, item) { IsUpsert = true };
                requests.Add(request);
            }

            return GetCollection<T>().BulkWriteAsync(requests);
        }

        public Task UpdateIncrementAsync<T, TField>(Expression<Func<T, bool>> expression,
            Expression<Func<T, TField>> field, TField value)
            where T : class, new()
        {
            var filter = Builders<T>.Filter.Where(expression);
            var update = Builders<T>.Update.Inc(field, value);

            return GetCollection<T>().FindOneAndUpdateAsync(filter, update);
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(string path, string query)
            where T : class, new()
        {
            var pathItems = path.Trim('[', ']', ' ').Split(',');
            var project = pathItems.Aggregate(string.Empty,
                (current, pathItem) => current + $"{pathItem}: 1,");

            var pipeline = new[]
            {
                BsonDocument.Parse(
                    $"{{ $search: {{ \"text\": {{\"path\": {path}, \"query\" : \"{query}\", \"fuzzy\" : {{\"maxEdits\": 1}} }} }} }}"),
                BsonDocument.Parse("{ $limit: 1000 }"),
                BsonDocument.Parse($"{{ $project: {{ {project} score: {{ $meta: \"searchScore\"}} }} }}"),
                BsonDocument.Parse("{ $match: { \"score\": {$gt: 0} } }")
            };

            return await GetCollection<T>().Aggregate<T>(pipeline).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAnyInAndWhereAsync<T, TField>(
            Expression<Func<T, IEnumerable<TField>>> field,
            IEnumerable<TField> inValues,
            Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            var filter = Builders<T>.Filter.Where(expression)
                         & Builders<T>.Filter.AnyIn(field, inValues);

            return await GetCollection<T>().Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAnyEqAndWhereAsync<T, TField>(
            Expression<Func<T, IEnumerable<TField>>> field,
            TField value,
            Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            var filter = Builders<T>.Filter.Where(expression)
                         & Builders<T>.Filter.AnyEq(field, value);

            return await GetCollection<T>().Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetInAndWhereAsync<T, TField>(
            Expression<Func<T, TField>> field,
            IEnumerable<TField> inValues,
            Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            var filter = Builders<T>.Filter.Empty;

            if (inValues != null)
            {
                filter &= Builders<T>.Filter.In(field, inValues);
            }

            if (expression != null)
            {
                filter &= Builders<T>.Filter.Where(expression);
            }

            return await GetCollection<T>().Find(filter).ToListAsync();
        }

        public Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            return GetCollection<T>()
                .Find(Builders<T>.Filter.Where(expression)).AnyAsync();
        }

        private IMongoCollection<T> GetCollection<T>()
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

            var extraElementsPack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
            ConventionRegistry.Register(nameof(IgnoreExtraElementsConvention),
                extraElementsPack, t => true);
        }
    }
}
using MongoDB.Driver;
using Moq;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class MongoRepositoryTests<TDocument>
    {
        protected readonly Mock<IMongoDatabase> Database;
        protected readonly Mock<IMongoCollection<TDocument>> Collection;

        public MongoRepositoryTests()
        {
            var connectionString = "mongodb://test";
            var client = new Mock<MongoClient>(connectionString);
            var urlBuilder = new Mock<MongoUrlBuilder>(connectionString);
            Database = new Mock<IMongoDatabase>();

            client.Setup(c => c.GetDatabase(urlBuilder.Object.DatabaseName, null))
                .Returns(Database.Object);

            Collection = new Mock<IMongoCollection<TDocument>>();

            Database.Setup(f => f.GetCollection<TDocument>(typeof(TDocument).Name, null))
                .Returns(Collection.Object);
        }
    }
}
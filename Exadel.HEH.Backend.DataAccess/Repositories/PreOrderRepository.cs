using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class PreOrderRepository : MongoRepository<PreOrder>
    {
        public PreOrderRepository(IMongoDatabase database)
            : base(database)
        {
        }
    }
}
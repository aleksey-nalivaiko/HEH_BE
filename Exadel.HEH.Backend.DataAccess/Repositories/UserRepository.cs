using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(IMongoDatabase database)
            : base(database)
        {
        }
    }
}
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }
    }
}
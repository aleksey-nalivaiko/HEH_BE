using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<User> Get()
        {
            return Context.GetAll<User>();
        }

        public Task<User> GetByEmail(string email)
        {
            return Context.GetAll<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
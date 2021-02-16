using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Get();

        Task<User> GetByEmail(string email);
    }
}
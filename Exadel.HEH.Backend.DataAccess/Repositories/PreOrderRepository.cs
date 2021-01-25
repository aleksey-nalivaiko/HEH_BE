using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class PreOrderRepository : MongoRepository<PreOrder>
    {
        public PreOrderRepository(IDbContext context)
            : base(context)
        {
        }
    }
}
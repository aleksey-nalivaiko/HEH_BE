using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class PreOrderRepository : BaseRepository<PreOrder>
    {
        public PreOrderRepository(IDbContext context)
            : base(context)
        {
        }
    }
}
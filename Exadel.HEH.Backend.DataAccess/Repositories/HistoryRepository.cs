using System.Linq;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<History> Get()
        {
            return Context.GetAll<History>();
        }
    }
}
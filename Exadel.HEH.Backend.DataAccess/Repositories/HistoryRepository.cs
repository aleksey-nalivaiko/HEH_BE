using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class HistoryRepository : MongoRepository<History>
    {
        public HistoryRepository(IDbContext context)
            : base(context)
        {
        }
    }
}
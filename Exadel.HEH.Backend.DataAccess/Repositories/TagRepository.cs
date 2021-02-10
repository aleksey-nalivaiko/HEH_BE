using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>
    {
        public TagRepository(IDbContext context)
            : base(context)
        {
        }
    }
}

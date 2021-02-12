using System.Collections.Generic;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class SearchRepository : MongoRepository<Search>, ISearchRepository
    {
        public SearchRepository(IDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Search> Search()
        {
            throw new System.NotImplementedException();
        }
    }
}
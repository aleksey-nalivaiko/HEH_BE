using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            return Context.GetAsync<Tag>(t => t.CategoryId == categoryId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            var tagCollection = Context.GetAll<Tag>()
                .Where(x => x.CategoryId.Equals(categoryId)).ToListAsync();
            return await tagCollection;
        }
    }
}

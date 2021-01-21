using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetByTagAsync(Guid tagId)
        {
            var categoryCollection = Context.GetAll<Category>();
            var tagCollection = Context.GetAll<Tag>();
            var tag = tagCollection.Where(x => x.Id == tagId).FirstOrDefault();
            var category = categoryCollection.Where(x => x.Id == tag.CategoryId);
            return await category.ToListAsync();
        }
    }
}

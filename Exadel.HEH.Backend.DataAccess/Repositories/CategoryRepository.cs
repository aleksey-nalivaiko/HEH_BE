using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;

using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<Category> GetByTagAsync(Guid tagId)
        {
            throw new NotImplementedException();

            //var categoryCollection = Context.GetAll<Category>(nameof(Category));
            //var tagCollection = Context.GetAll<Tag>(nameof(Tag));
            //var tag = tagCollection
            //    .Find(Builders<Tag>.Filter.Eq(x => x.Id, tagId))
            //    .FirstOrDefaultAsync();
            //return categoryCollection
            //    .Find(Builders<Category>.Filter.Eq(x => x.Id, tag.Result.Id))
            //    .FirstOrDefaultAsync();
        }
    }
}

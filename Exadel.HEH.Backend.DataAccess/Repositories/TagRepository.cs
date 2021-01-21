using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using MongoDB.Driver;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context)
            : base(context)
        {
            return GetAllBaseAsync();
        }

        public async Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();

            //var tagCollection = Context.GetAll<Tag>(nameof(Tag));
            //return await tagCollection
            //    .Find(Builders<Tag>.Filter.Eq(x => x.CategoryId, categoryId))
            //    .ToListAsync();
        }
    }
}

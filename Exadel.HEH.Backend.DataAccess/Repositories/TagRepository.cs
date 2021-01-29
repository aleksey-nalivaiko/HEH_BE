using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : MongoRepository<Tag>, ITagRepository
    {
        private readonly IDiscountRepository _discountRepository;

        public TagRepository(IDbContext context)
            : base(context)
        {
        }

        public TagRepository(IDbContext context, IDiscountRepository discountRepository)
            : base(context)
        {
            _discountRepository = discountRepository;
        }

        public async Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            var tagCollection = Context.GetAll<Tag>()
                .Where(x => x.CategoryId.Equals(categoryId)).ToListAsync();
            return await tagCollection;
        }

        public override Task RemoveAsync(Guid id)
        {
            _discountRepository.RemoveTagsFromDiscounts(id);
            return base.RemoveAsync(id);
        }
    }
}

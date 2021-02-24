using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbContext context)
            : base(context)
        {
        }

        public Task<IEnumerable<Tag>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return Context.GetAsync<Tag>(t => ids.Contains(t.Id));
        }

        public Task<IEnumerable<Tag>> GetAsync(Expression<Func<Tag, bool>> expression)
        {
            return Context.GetAsync(expression);
        }

        public Task RemoveAsync(Expression<Func<Tag, bool>> expression)
        {
            return Context.RemoveAsync(expression);
        }
    }
}

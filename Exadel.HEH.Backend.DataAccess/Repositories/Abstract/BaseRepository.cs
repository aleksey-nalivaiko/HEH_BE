using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public abstract class BaseRepository<TDocument> : IRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        protected readonly IDbContext Context;

        protected BaseRepository(IDbContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return await Context.GetAll<TDocument>().ToListAsync();
        }

        public virtual Task<TDocument> GetByIdAsync(Guid id)
        {
            return Context.GetByIdAsync<TDocument>(id);
        }

        public virtual Task RemoveAsync(Guid id)
        {
            return Context.RemoveAsync<TDocument>(id);
        }

        public virtual Task CreateAsync(TDocument item)
        {
            return Context.CreateAsync(item);
        }

        public virtual Task UpdateAsync(TDocument item)
        {
            return Context.UpdateAsync(item);
        }
    }
}
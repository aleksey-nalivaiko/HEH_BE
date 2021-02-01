using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IDbContext _context;

        public IdentityRepository(IDbContext context)
        {
            _context = context;
        }

        public Task CreateAsync<TDocument>(TDocument item)
            where TDocument : class, new()
        {
            return _context.CreateAsync(item);
        }

        public Task<bool> AnyAsync<TDocument>()
            where TDocument : class, new()
        {
            return _context.AnyAsync<TDocument>();
        }

        public Task RemoveAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new()
        {
            return _context.RemoveAsync(expression);
        }

        public async Task<IEnumerable<TDocument>> GetAllAsync<TDocument>()
            where TDocument : class, new()
        {
            return await _context.GetAll<TDocument>().ToListAsync();
        }

        public Task<IEnumerable<TDocument>> GetAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new()
        {
            return _context.GetAsync(expression);
        }

        public Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new()
        {
            return _context.GetOneAsync(expression);
        }
    }
}
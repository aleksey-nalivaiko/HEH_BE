using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<TDocument>> GetAllAsync<TDocument>()
            where TDocument : class, new();

        Task<IEnumerable<TDocument>> GetAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new();

        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new();

        Task RemoveAsync<TDocument>(Expression<Func<TDocument, bool>> expression)
            where TDocument : class, new();

        Task CreateAsync<TDocument>(TDocument item)
            where TDocument : class, new();

        Task RemoveAllAsync<T>()
            where T : class, new();
    }
}
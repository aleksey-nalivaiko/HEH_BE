using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess
{
    public interface IDbContext
    {
        IQueryable<T> GetAll<T>()
            where T : class, new();

        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new();

        Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IDataModel, new();

        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new();

        Task RemoveAsync<T>(Guid id)
            where T : class, IDataModel, new();

        Task RemoveAsync<T>(Expression<Func<T, bool>> expression)
            where T : class, new();

        Task CreateAsync<T>(T item)
            where T : class, new();

        Task CreateManyAsync<T>(IEnumerable<T> items)
            where T : class, new();

        Task UpdateAsync<T>(T item)
            where T : class, IDataModel, new();

        Task UpdateManyAsync<T>(IEnumerable<T> items)
            where T : class, IDataModel, new();

        Task UpdateIncrementAsync<T, TField>(Guid id, Expression<Func<T, TField>> field, TField value)
            where T : class, IDataModel, new();

        Task<bool> AnyAsync<T>()
            where T : class, new();
    }
}
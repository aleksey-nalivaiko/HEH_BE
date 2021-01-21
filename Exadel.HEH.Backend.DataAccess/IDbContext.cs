using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess
{
    public interface IDbContext
    {
        IQueryable<T> GetAll<T>()
            where T : class, IDataModel, new();

        Task<T> GetByIdAsync<T>(Guid id)
            where T : class, IDataModel, new();

        Task RemoveAsync<T>(Guid id)
            where T : class, IDataModel, new();

        Task CreateAsync<T>(T item)
            where T : class, IDataModel, new();

        Task UpdateAsync<T>(T item)
            where T : class, IDataModel, new();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    public interface IController<T>
        where T : class, IDataModel, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task CreateAsync(T item);

        Task UpdateAsync(Guid id, T item);
    }
}
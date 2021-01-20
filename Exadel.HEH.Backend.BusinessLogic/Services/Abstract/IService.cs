using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task CreateAsync(T item);

        Task UpdateAsync(Guid id, T item);
    }
}
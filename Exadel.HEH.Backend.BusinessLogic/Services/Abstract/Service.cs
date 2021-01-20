using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public class Service<T> : IService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, T item)
        {
            throw new NotImplementedException();
        }
    }
}
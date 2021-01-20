using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public abstract class Service<T> : IService<T>
        where T : class, IDataModel, new()
    {
        protected readonly IRepository<T> Repository;

        protected Service(IRepository<T> repository)
        {
            Repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Repository.GetAllAsync();
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
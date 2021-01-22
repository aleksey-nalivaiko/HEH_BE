using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public abstract class BaseService<T> : IService<T>
        where T : class, IDataModel, new()
    {
        protected readonly IRepository<T> Repository;

        protected BaseService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task RemoveAsync(Guid id)
        {
            return Repository.RemoveAsync(id);
        }

        public Task CreateAsync(T item)
        {
            return Repository.CreateAsync(item);
        }

        public Task UpdateAsync(T item)
        {
            return Repository.UpdateAsync(item);
        }
    }
}
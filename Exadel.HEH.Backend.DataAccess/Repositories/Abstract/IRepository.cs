using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        Task<IEnumerable<TDocument>> GetAllAsync();

        Task<TDocument> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task CreateAsync(TDocument item);

        Task UpdateAsync(TDocument item);
    }
}
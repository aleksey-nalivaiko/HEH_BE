using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public interface IMongoRepository<TDocument>
        where TDocument : class, IDataModel, new()
    {
        Task<IEnumerable<TDocument>> GetAllBaseAsync();

        Task<TDocument> GetByIdBaseAsync(Guid id);

        Task RemoveBaseAsync(Guid id);

        Task CreateBaseAsync(TDocument item);

        Task UpdateBaseAsync(Guid id, TDocument item);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class HistoryRepository : MongoRepository<History>
    {
        public HistoryRepository(string connectionString)
            : base(connectionString)
        {
        }

        public Task CreateAsync(History historyItem)
        {
            return CreateBaseAsync(historyItem);
        }

        public Task<IEnumerable<History>> GetAllAsync()
        {
            return GetAllBaseAsync();
        }

        public Task RemoveAsync(Guid id)
        {
            return RemoveBaseAsync(id);
        }

        public Task UpdateAsync(Guid id, History historyItem)
        {
            return UpdateBaseAsync(id, historyItem);
        }
    }
}
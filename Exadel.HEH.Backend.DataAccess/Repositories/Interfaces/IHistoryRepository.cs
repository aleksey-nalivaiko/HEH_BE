using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public interface IHistoryRepository
    {
        Task CreateAsync(History historyItem);

        Task<IEnumerable<History>> GetAllAsync();

        Task RemoveAsync(Guid id);

        Task UpdateAsync(Guid id, History historyItem);
    }
}
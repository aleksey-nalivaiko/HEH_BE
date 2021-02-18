using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IHistoryService : IService<HistoryDto>
    {
        Task CreateAsync(UserAction action, string description);

        IQueryable<HistoryDto> Get();
    }
}
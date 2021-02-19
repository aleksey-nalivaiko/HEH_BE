using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNet.OData.Query;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IHistoryService
    {
        Task CreateAsync(UserAction action, string description);

        Task<IEnumerable<HistoryDto>> GetAllAsync(ODataQueryOptions<HistoryDto> options);
    }
}
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class HistoryService : BaseService<History, HistoryDto>
    {
        public HistoryService(IRepository<History> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
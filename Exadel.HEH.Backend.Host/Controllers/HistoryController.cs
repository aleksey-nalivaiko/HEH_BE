using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Exadel.HEH.Backend.Host.DTOs.Get;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class HistoryController : BaseController<History, HistoryDto, HistoryCreateDto, HistoryCreateDto>
    {
        public HistoryController(IService<History> service, IMapper mapper)
            : base(service, mapper)
        {
        }
    }
}
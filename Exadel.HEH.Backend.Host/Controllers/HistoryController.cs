using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public class HistoryController : BaseController<HistoryDto>
    {
        public HistoryController(IService<HistoryDto> service)
            : base(service)
        {
        }
    }
}
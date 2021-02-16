﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class HistoryController : BaseController<HistoryDto>
    {
        public HistoryController(IHistoryService service)
            : base(service)
        {
        }

        [Authorize(Roles = nameof(UserRole.Administrator))]
        public override Task<IEnumerable<HistoryDto>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
    }
}
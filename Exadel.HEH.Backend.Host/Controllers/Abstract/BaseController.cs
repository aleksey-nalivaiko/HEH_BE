using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public abstract class BaseController<TDto> : ControllerBase
        where TDto : class, new()
    {
        protected readonly IService<TDto> Service;

        protected BaseController(IService<TDto> service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual Task<IEnumerable<TDto>> GetAllAsync()
        {
            return Service.GetAllAsync();
        }
    }
}
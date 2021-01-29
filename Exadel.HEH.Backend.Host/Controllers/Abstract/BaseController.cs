using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TDto> : ControllerBase,
        IController<TDto>
        where TDto : class, new()
    {
        protected readonly IService<TDto> Service;

        protected BaseController(IService<TDto> service)
        {
            Service = service;
        }

        [HttpGet]
        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            return Service.GetAllAsync();
        }
    }
}
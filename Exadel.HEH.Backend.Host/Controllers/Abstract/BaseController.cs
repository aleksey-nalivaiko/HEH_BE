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

        [HttpGet("{id:guid}")]
        public Task<TDto> GetByIdAsync(Guid id)
        {
            return Service.GetByIdAsync(id);
        }

        [HttpDelete("{id:guid}")]
        public Task RemoveAsync(Guid id)
        {
            return Service.RemoveAsync(id);
        }

        //[HttpPost]
        //public async Task CreateAsync(TCreateDto item)
        //{
        //    await Service.CreateAsync(Mapper.Map<T>(item));
        //}

        //[HttpPut]
        //public async Task UpdateAsync(TUpdateDto item)
        //{
        //    await Service.UpdateAsync(Mapper.Map<T>(item));
        //}
    }
}
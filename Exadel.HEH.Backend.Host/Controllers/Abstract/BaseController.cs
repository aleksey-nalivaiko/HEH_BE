using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T, TDto, TCreateDto, TUpdateDto> : ControllerBase,
        IController<T, TDto, TCreateDto, TUpdateDto>
        where T : class, IDataModel, new()
    {
        protected readonly IService<T> Service;
        protected readonly IMapper Mapper;

        protected BaseController(IService<T> service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var result = await Service.GetAllAsync();
            return Mapper.Map<IEnumerable<TDto>>(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await Service.GetByIdAsync(id);
            return Mapper.Map<TDto>(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task RemoveAsync(Guid id)
        {
            await Service.RemoveAsync(id);
        }

        [HttpPost]
        public async Task CreateAsync(TCreateDto item)
        {
            await Service.CreateAsync(Mapper.Map<T>(item));
        }

        [HttpPut]
        public async Task UpdateAsync(TUpdateDto item)
        {
            await Service.UpdateAsync(Mapper.Map<T>(item));
        }
    }
}
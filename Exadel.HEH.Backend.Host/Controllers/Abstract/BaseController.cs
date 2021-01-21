using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase, IController<T>
        where T : class, IDataModel, new()
    {
        protected readonly IService<T> Service;

        protected BaseController(IService<T> service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Service.GetAllAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Service.GetByIdAsync(id);
        }

        [HttpPut("{id:guid}")]
        public async Task RemoveAsync(Guid id)
        {
            await Service.RemoveAsync(id);
        }

        [HttpPost]
        public async Task CreateAsync(T item)
        {
            await Service.CreateAsync(item);
        }

        [HttpPut("{id:guid}")]
        public async Task UpdateAsync(Guid id, T item)
        {
            await Service.UpdateAsync(id, item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host
{
    [Route("api/Category/[controller]")]
    [ApiController]
    public class ApiCategoryController : ControllerBase
    {
        [HttpPost]
        public Task CreateAsynk([FromBody] Category categoryItem)
        {
            return null;
        }

        [HttpGet]
        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return null;
        }

        [HttpGet("{id}")]
        public Task<Category> GetByIdAsync(Guid id)
        {
            return null;
        }

        [HttpGet("{id}")]
        public Task<Category> GetByTagAsync(Guid tagId)
        {
            return null;
        }

        [HttpDelete("{id}")]
        public Task RemoveAsync(Guid id)
        {
            return null;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(Guid id, [FromBody] Category categoryItem)
        {
            return null;
        }
    }
}

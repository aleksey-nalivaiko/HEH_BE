using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        [HttpPost]
        public Task CreateAsync([FromBody] History historyItem)
        {
            return null;
        }

        [HttpGet]
        public Task<IEnumerable<History>> GetAllAsync()
        {
            return null;
        }

        [HttpDelete("{id}")]
        public Task RemoveAsync(Guid id)
        {
            return null;
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(Guid id, [FromBody] History historyItem)
        {
            return null;
        }
    }
}

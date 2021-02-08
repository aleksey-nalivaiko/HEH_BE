using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Moderator))]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete]
        public Task RemoveAsync(Guid id)
        {
            return _tagService.RemoveAsync(id);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(TagDto item)
        {
            if (ModelState.IsValid)
            {
                await _tagService.CreateAsync(item);
                return Created(string.Empty, item);
            }

            return BadRequest(ModelState);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(TagDto item)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateAsync(item);
                return Created(string.Empty, item);
            }

            return BadRequest(ModelState);
        }
    }
}
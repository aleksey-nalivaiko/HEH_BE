using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
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
        private readonly ITagValidationService _validationService;

        public TagController(ITagService tagService, ITagValidationService validationService)
        {
            _tagService = tagService;
            _validationService = validationService;
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveAsync(Guid id)
        {
            if (await _validationService.TagExistsAsync(id))
            {
                await _tagService.RemoveAsync(id);
                return Ok();
            }

            return NotFound();
        }

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

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(TagDto item)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateAsync(item);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
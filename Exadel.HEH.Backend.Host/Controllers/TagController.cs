using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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

        [HttpDelete]
        public Task RemoveAsync(Guid id)
        {
            return _tagService.RemoveAsync(id);
        }

        [HttpPost]
        public Task CreateAsync(TagDto item)
        {
            return _tagService.CreateAsync(item);
        }

        [HttpPut]
        public Task UpdateAsync(TagDto item)
        {
            return _tagService.UpdateAsync(item);
        }
    }
}
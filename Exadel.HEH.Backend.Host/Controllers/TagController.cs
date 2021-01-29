using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
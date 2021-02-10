using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryValidationService _validationService;

        public CategoryController(ICategoryService categoryService, ICategoryValidationService validationService)
        {
            _validationService = validationService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync()
        {
            return _categoryService.GetCategoriesWithTagsAsync();
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> RemoveAsync(Guid id)
        {
            if (await _validationService.DiscountContainsCategory(id))
            {
                await _categoryService.RemoveAsync(id);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> CreateAsync(CategoryDto item)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(item);
                return Created(string.Empty, item);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> UpdateAsync(CategoryDto item)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(item);
                return Created(string.Empty, item);
            }

            return BadRequest(ModelState);
        }
    }
}
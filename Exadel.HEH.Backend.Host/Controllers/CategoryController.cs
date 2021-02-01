using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Identity;
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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync()
        {
            return _categoryService.GetCategoriesWithTagsAsync();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Moderator)]
        public Task RemoveAsync(Guid id)
        {
            return _categoryService.RemoveAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Moderator)]
        public Task CreateAsync(CategoryDto item)
        {
           return _categoryService.CreateAsync(item);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Moderator)]
        public Task UpdateAsync(CategoryDto item)
        {
            return _categoryService.UpdateAsync(item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        public Task RemoveAsync(Guid id)
        {
            return _categoryService.RemoveAsync(id);
        }

        [HttpPost]
        public Task CreateAsync(CategoryDto item)
        {
           return _categoryService.CreateAsync(item);
        }

        [HttpPut]
        public Task UpdateAsync(CategoryDto item)
        {
            return _categoryService.UpdateAsync(item);
        }
    }
}
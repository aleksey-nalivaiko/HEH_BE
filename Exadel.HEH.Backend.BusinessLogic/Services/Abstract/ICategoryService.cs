using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync();

        Task<CategoryDto> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task CreateAsync(CategoryDto item);

        Task UpdateAsync(CategoryDto item);
    }
}
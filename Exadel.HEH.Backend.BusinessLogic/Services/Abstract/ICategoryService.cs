using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync();
    }
}
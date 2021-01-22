using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.Host.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryWithTagsDto>> GetCategoriesWithTagsAsync();
    }
}
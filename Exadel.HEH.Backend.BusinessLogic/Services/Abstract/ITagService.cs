using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ITagService : IService<TagDto>
    {
        Task<IEnumerable<TagDto>> GetByCategoryAsync(Guid categoryId);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetByIds(IEnumerable<Guid> ids);

        Task RemoveAsync(Guid id);

        Task CreateAsync(TagDto item);

        Task UpdateAsync(TagDto item);
    }
}

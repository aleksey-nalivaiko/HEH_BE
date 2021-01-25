using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ITagService : IService<Tag>
    {
        Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId);
    }
}

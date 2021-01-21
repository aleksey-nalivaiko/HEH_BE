using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ICategoryService : IService<Category>
    {
        Task<IEnumerable<Category>> GetByTagAsync(Guid tagId);
    }
}

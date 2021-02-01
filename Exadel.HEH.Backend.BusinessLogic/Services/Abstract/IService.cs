using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IService<TDto>
        where TDto : class, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();
    }
}
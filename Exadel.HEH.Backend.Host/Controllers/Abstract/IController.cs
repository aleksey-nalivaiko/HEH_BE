using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    public interface IController<TDto>
        where TDto : class, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();
    }
}
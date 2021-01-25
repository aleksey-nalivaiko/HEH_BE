using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    public interface IController<TDto>
        where TDto : class, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        //Task CreateAsync(TCreateDto item);

        //Task UpdateAsync(TUpdateDto item);
    }
}
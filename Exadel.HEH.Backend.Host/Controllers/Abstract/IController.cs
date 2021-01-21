using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.Host.Controllers.Abstract
{
    public interface IController<T, TDto, TCreateDto, TUpdateDto>
        where T : class, IDataModel, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task CreateAsync(TCreateDto item);

        Task UpdateAsync(Guid id, TUpdateDto item);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IService<TDto>
        where TDto : class, new()
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        //Task CreateAsync(TDto item);

        //Task UpdateAsync(TDto item);
    }
}
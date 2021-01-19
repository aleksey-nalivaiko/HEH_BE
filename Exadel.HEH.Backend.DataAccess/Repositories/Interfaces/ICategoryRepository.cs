using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category categoryItem);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task UpdateAsync(Guid id, Category categoryItem);

        Task<Category> GetByTagAsync(Guid tagId);
    }
}

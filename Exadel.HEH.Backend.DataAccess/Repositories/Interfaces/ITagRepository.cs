using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.Example.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public interface ITagRepository
    {
        Task CreateAsync(Tag tagItem);

        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task UpdateAsync(Guid id, Tag tagItem);

        Task<IEnumerable<Tag>> GetByCategotyAsync(Guid categoryId);
    }
}

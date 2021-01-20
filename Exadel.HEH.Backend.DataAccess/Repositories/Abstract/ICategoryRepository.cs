using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByTagAsync(Guid tagId);
    }
}
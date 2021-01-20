using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public Task CreateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByTagAsync(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Category item)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly ICategoryValidationService _validationCategoryService;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,
            ITagRepository tagRepository,
            ICategoryValidationService validationCategoryService,
            IMapper mapper)
        {
            _validationCategoryService = validationCategoryService;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync()
        {
            var tags = (await _tagRepository.GetAllAsync()).ToList();
            var categories = (await _categoryRepository.GetAllAsync()).ToList();

            var categoriesWithTags = categories.GroupJoin(
                tags,
                category => category.Id,
                tag => tag.CategoryId,
                (category, categoryTags) => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Tags = _mapper.Map<IEnumerable<TagDto>>(categoryTags)
                });

            return await Task.FromResult(categoriesWithTags);
        }

        public async Task CreateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.CreateAsync(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _categoryRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.UpdateAsync(result);
        }
    }
}

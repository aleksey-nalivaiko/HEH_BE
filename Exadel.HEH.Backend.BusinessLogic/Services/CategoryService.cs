using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly ITagService _tagService;

        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,
            ITagRepository tagRepository, IMapper mapper, ITagService tagService)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _tagService = tagService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesWithTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();

            var categoriesWithTags = new List<CategoryDto>();
            var categoryDictionary = new Dictionary<Guid, List<TagDto>>();

            foreach (var tag in tags)
            {
                if (!categoryDictionary.ContainsKey(tag.CategoryId))
                {
                    categoryDictionary.Add(tag.CategoryId, new List<TagDto>());
                }

                categoryDictionary[tag.CategoryId].Add(_mapper.Map<TagDto>(tag));
            }

            foreach (var categoryTags in categoryDictionary)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryTags.Key);
                categoriesWithTags.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Tags = categoryTags.Value
                });
            }

            return await Task.FromResult(categoriesWithTags);
        }

        public async Task CreateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.CreateAsync(result);
        }

        public async Task<bool> RemoveAsync(Guid id) //TODO: Add Task<Result>
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var tags = await _tagService.GetByCategoryAsync(category.Id);
            if (tags is null)
            {
                await _categoryRepository.RemoveAsync(id);
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task UpdateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.UpdateAsync(result);
        }
    }
}

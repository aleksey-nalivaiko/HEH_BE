using System;
using System.Collections.Generic;
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
        private readonly IValidationCategoryService _validationCategoryService;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,
            ITagRepository tagRepository,
            IValidationCategoryService validationCategoryService,
            IMapper mapper)
        {
            _validationCategoryService = validationCategoryService;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
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

        public async Task RemoveAsync(Guid id)
        {
            if (!await _validationCategoryService.DiscountContainsCategory(id))
            {
                await _categoryRepository.RemoveAsync(id);
            }
        }

        public async Task UpdateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.UpdateAsync(result);
        }
    }
}

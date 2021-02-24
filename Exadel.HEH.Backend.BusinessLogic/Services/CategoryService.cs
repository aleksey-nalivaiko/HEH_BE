using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;

        public CategoryService(ICategoryRepository categoryRepository, IHistoryService historyService,
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            _historyService = historyService;
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

        public async Task<CategoryDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var result = await _categoryRepository.GetByIdsAsync(ids);
            return _mapper.Map<IEnumerable<CategoryDto>>(result);
        }

        public async Task CreateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.CreateAsync(result);
            await _historyService.CreateAsync(UserAction.Add,
                "Created category " + result.Name);
        }

        public async Task RemoveAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.RemoveAsync(id);
            await _historyService.CreateAsync(UserAction.Remove,
                "Removed category " + category.Name);

            Expression<Func<Tag, bool>> expression = t => t.CategoryId == id;
            var tags = await _tagRepository.GetAsync(expression);
            await _tagRepository.RemoveAsync(expression);

            foreach (var tag in tags)
            {
                await _historyService.CreateAsync(UserAction.Remove,
                    "Removed tag " + tag.Name);
            }
        }

        public async Task UpdateAsync(CategoryDto item)
        {
            var result = _mapper.Map<Category>(item);
            await _categoryRepository.UpdateAsync(result);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated category " + result.Name);
        }
    }
}

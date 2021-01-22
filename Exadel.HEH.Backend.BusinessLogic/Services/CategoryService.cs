using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Exadel.HEH.Backend.Host.DTOs.Get;
using Tag = Exadel.HEH.Backend.DataAccess.Models.Tag;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository,
            IRepository<Tag> tagRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryWithTagsDto>> GetCategoriesWithTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();

            var categoriesWithTags = new List<CategoryWithTagsDto>();
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
                categoriesWithTags.Add(new CategoryWithTagsDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Tags = categoryTags.Value
                });
            }

            return await Task.FromResult(categoriesWithTags);
        }
    }
}

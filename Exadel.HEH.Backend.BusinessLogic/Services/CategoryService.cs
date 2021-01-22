using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<IEnumerable<CategoryWithTagsDto>> GetCategoryWithTagsAsync()
        {
            var categoriesTask = _categoryRepository.GetAllAsync();
            var tagsTask = _tagRepository.GetAllAsync();

            var categories = await categoriesTask;
            var tags = await tagsTask;

            var categoriesWithTags = new List<CategoryWithTagsDto>();

            var c = new Dictionary<Guid, List<TagDto>>();
            foreach (var tag in tags)
            {
                if (c.ContainsKey(tag.CategoryId))
                {
                    c[tag.CategoryId].Add(_mapper.Map<TagDto>(tag));
                }
            }

            return await Task.FromResult(categoriesWithTags);
        }
    }
}

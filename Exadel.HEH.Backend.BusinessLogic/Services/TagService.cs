using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class TagService : BaseService<Tag, TagDto>, ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository, IMapper mapper)
            : base(tagRepository, mapper)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagDto>> GetByCategoryAsync(Guid categoryId)
        {
            var result = await _tagRepository.GetByCategoryAsync(categoryId);
            return Mapper.Map<IEnumerable<TagDto>>(result);
        }

        public async Task CreateAsync(TagDto item)
        {
            var result = Mapper.Map<Tag>(item);
            await _tagRepository.CreateAsync(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _tagRepository.RemoveAsync(id);
        }

        public async Task UpdateAsync(TagDto item)
        {
            var result = Mapper.Map<Tag>(item);
            await _tagRepository.UpdateAsync(result);
        }
    }
}

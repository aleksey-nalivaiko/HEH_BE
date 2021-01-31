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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IRepository<Tag> _repository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> repository, ITagRepository tagRepository, IDiscountRepository discountRepository, IMapper mapper)
        {
            _repository = repository;
            _tagRepository = tagRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>> GetByCategoryAsync(Guid categoryId)
        {
            var result = await _tagRepository.GetByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<TagDto>>(result);
        }

        public async Task CreateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _repository.CreateAsync(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _repository.RemoveAsync(id);
            var collection = await _discountRepository.GetAllAsync();
            foreach (var item in collection)
            {
                await Task.Run(() => item.TagsIds.Remove(id));
                await _discountRepository.UpdateAsync(item);
            }
        }

        public async Task UpdateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _repository.UpdateAsync(result);
        }

        public Task<IEnumerable<TagDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TagDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;

        public TagService(ITagRepository tagRepository, IDiscountRepository discountRepository, IMapper mapper, IHistoryService historyService)
        {
            _tagRepository = tagRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _historyService = historyService;
        }

        public async Task CreateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.CreateAsync(result);
            await _historyService.CreateAsync(UserAction.Add,
                "Created tag " + result.Id);
        }

        public async Task<IEnumerable<TagDto>> GetByIds(IEnumerable<Guid> ids)
        {
            var result = await _tagRepository.GetByIds(ids);
            return _mapper.Map<IEnumerable<TagDto>>(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _tagRepository.RemoveAsync(id);
            var discounts = await _discountRepository.GetAllAsync();
            foreach (var discount in discounts)
            {
                discount.TagsIds.Remove(id);
                await _discountRepository.UpdateAsync(discount);
            }

            await _historyService.CreateAsync(UserAction.Remove,
                "Removed tag " + id);
        }

        public async Task UpdateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.UpdateAsync(result);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated tag " + result.Id);
        }
    }
}

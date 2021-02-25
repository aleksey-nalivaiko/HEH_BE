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
    public class TagService : BaseService<Tag, TagDto>, ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;
        private readonly IUserService _userService;

        public TagService(
            ITagRepository tagRepository,
            IDiscountRepository discountRepository,
            IMapper mapper,
            IHistoryService historyService,
            IUserService userService)
            : base(tagRepository, mapper)
        {
            _tagRepository = tagRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _historyService = historyService;
            _userService = userService;
        }

        public async Task CreateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.CreateAsync(result);
            await _historyService.CreateAsync(UserAction.Add,
                "Created tag " + result.Name);
        }

        public async Task RemoveByCategoryAsync(Guid categoryId)
        {
            Expression<Func<Tag, bool>> expression = t => t.CategoryId == categoryId;

            var tags = await _tagRepository.GetAsync(expression);
            await _tagRepository.RemoveAsync(expression);

            var discounts = (await _discountRepository.GetAllAsync()).ToList();
            var removeTasks = tags.Select(t => PostRemoveAsync(t, discounts));

            await Task.WhenAll(removeTasks);
        }

        public async Task<IEnumerable<TagDto>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var result = await _tagRepository.GetByIdsAsync(ids);
            return _mapper.Map<IEnumerable<TagDto>>(result);
        }

        public async Task RemoveAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            await _tagRepository.RemoveAsync(id);

            var discounts = await _discountRepository.GetAllAsync();
            await PostRemoveAsync(tag, discounts.ToList());
        }

        public async Task UpdateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.UpdateAsync(result);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated tag " + result.Name);
        }

        private async Task PostRemoveAsync(Tag tag, IList<Discount> discounts)
        {
            foreach (var discount in discounts)
            {
                discount.TagsIds.Remove(tag.Id);
            }

            await _discountRepository.UpdateManyAsync(discounts);

            await _historyService.CreateAsync(UserAction.Remove,
                "Removed tag " + tag.Name);

            await _userService.RemoveTagSubscriptionsAsync(tag.Id);
        }
    }
}

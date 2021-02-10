using System;
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
        private readonly IRepository<Tag> _tagRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> tagRepository, IDiscountRepository discountRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.CreateAsync(result);
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
        }

        public async Task UpdateAsync(TagDto item)
        {
            var result = _mapper.Map<Tag>(item);
            await _tagRepository.UpdateAsync(result);
        }
    }
}

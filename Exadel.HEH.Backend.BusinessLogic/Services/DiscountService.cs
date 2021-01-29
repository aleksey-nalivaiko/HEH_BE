using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public IQueryable<DiscountDto> Get(string searchText)
        {
            var discounts = _discountRepository.Get();
            if (searchText != null)
            {
                var lowerSearchText = searchText.ToLower();
                discounts = discounts.Where(d => d.Conditions.ToLower().Contains(lowerSearchText)
                    || d.VendorName.ToLower().Contains(lowerSearchText));
            }

            return discounts.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider);
        }

        public async Task<DiscountDto> GetByIdAsync(Guid id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            return _mapper.Map<DiscountDto>(discount);
        }
    }
}
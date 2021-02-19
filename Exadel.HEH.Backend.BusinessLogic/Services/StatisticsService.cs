using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.OData.Edm;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly IMapper _mapper;
        private readonly ISearchService<Discount, Discount> _searchService;

        public StatisticsService(IStatisticsRepository statisticsRepository,
            IMapper mapper,
            ISearchService<Discount, Discount> searchService)
        {
            _statisticsRepository = statisticsRepository;
            _mapper = mapper;
            _searchService = searchService;
        }

        public async Task<IQueryable<DiscountStatisticsDto>> GetStatisticsAsync(
            string searchText, DateTime startDate, DateTime endDate)
        {
            var discounts = await _searchService.SearchAsync(searchText);

            var discountsDto = _mapper.Map<IEnumerable<DiscountStatisticsDto>>(discounts);

            var discountViewsAmounts = await _statisticsRepository.GetInWhereAsync(
                s => s.DiscountId, discounts.Select(d => d.Id), startDate, endDate);

            return discountsDto.GroupJoin(
                    discountViewsAmounts,
                    d => d.Id,
                    s => s.DiscountId,
                    (d, statistics) =>
                    {
                        d.ViewsAmount = statistics.Sum(s => s.ViewsAmount);
                        return d;
                    }).AsQueryable();
        }

        public Task IncrementViewsAmountAsync(Guid discountId)
        {
            return _statisticsRepository.UpdateIncrementAsync(discountId, d => d.ViewsAmount, 1);
        }
    }
}
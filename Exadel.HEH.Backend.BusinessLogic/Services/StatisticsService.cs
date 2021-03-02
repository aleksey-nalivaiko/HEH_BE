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
using Microsoft.AspNet.OData.Query;

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
            ODataQueryOptions<DiscountStatisticsDto> options,
            string searchText, DateTime startDate, DateTime endDate)
        {
            var discounts = await _searchService.SearchAsync(searchText);

            var discountsDto = _mapper.Map<IEnumerable<DiscountStatisticsDto>>(discounts);

            var discountViewsAmounts = await _statisticsRepository.GetInWhereAsync(
                s => s.DiscountId, discounts.Select(d => d.Id), startDate, endDate);

            var discountsQueryable = discountsDto.GroupJoin(
                    discountViewsAmounts,
                    d => d.Id,
                    s => s.DiscountId,
                    (d, statistics) =>
                    {
                        d.ViewsAmount = statistics.Sum(s => s.ViewsAmount);
                        return d;
                    }).AsQueryable();

            if (options.Filter != null)
            {
                return options.ApplyTo(discountsQueryable) as IQueryable<DiscountStatisticsDto>;
            }

            return discountsQueryable;
        }

        public async Task IncrementViewsAmountAsync(Guid discountId)
        {
            var dateNow = DateTime.UtcNow.Date;

            Expression<Func<Statistics, bool>> expression = s => s.DiscountId == discountId
                                                                 && s.DateTime == dateNow;

            if (await _statisticsRepository.StatisticsExists(expression))
            {
                await _statisticsRepository.UpdateIncrementAsync(expression, d => d.ViewsAmount, 1);
            }
            else
            {
                await _statisticsRepository.CreateAsync(new Statistics
                {
                    DiscountId = discountId,
                    DateTime = dateNow,
                    ViewsAmount = 1
                });
            }
        }

        public Task RemoveAsync(Guid discountId)
        {
            return _statisticsRepository.RemoveAsync(s => s.DiscountId == discountId);
        }
    }
}